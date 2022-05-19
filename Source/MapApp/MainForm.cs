using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapInfo.Mapping;
using System.IO;
using MapInfo.Geometry;
using MapInfo.Styles;
using MapInfo.Data;
using MapInfo.Tools;

namespace MapApp
{
    public partial class MainForm : Form
    {
        #region 定义
        CoordSys cs;
        FeatureLayer equipLayer;
        BitmapPointStyle NodeStyle;
        DPoint DPLast;
        IResultSetFeatureCollection fSelectCollection;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            mapControl1.Map.ViewChangedEvent += new MapInfo.Mapping.ViewChangedEventHandler(Map_ViewChangedEvent);
            Map_ViewChangedEvent(this, null);
            mapControl1.Tools.FeatureSelected += new FeatureSelectedEventHandler(Tools_FeatureSelected);
        }

        void Map_ViewChangedEvent(object sender, MapInfo.Mapping.ViewChangedEventArgs e)
        {
            // Display the zoom level
            Double dblZoom = System.Convert.ToDouble(String.Format("{0:E2}", mapControl1.Map.Zoom.Value));
            if (statusStrip1.Items.Count > 0)
            {
                statusStrip1.Items[0].Text = "缩放: " + dblZoom.ToString() + " " + MapInfo.Geometry.CoordSys.DistanceUnitAbbreviation(mapControl1.Map.Zoom.Unit);
            }
            if (mapControl1.Map != null)
            {
                toolStripStatusLabel2.Text = String.Format(" x={0},y={1}", mapControl1.Map.Center.x, mapControl1.Map.Center.y);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.LoadMap();
            this.LoadEquip();
        }

        void LoadMap()
        {
            string MapPath = Path.Combine(Application.StartupPath, @"map\map.mws");
            MapWorkSpaceLoader mwsLoader = new MapWorkSpaceLoader(MapPath);
            mapControl1.Map.Load(mwsLoader);
            foreach (IMapLayer flTemp in mapControl1.Map.Layers)
            {
                LayerHelper.SetEditable(flTemp, false);
                LayerHelper.SetSelectable(flTemp, false);
            }

            //foreach (LabelLayer lbLayerTemp in mapControl1.Map.Layers)
            //{
            //    LayerHelper.SetEditable(lbLayerTemp, false);
            //    LayerHelper.SetSelectable(lbLayerTemp, false);
            //}
        }
        
        void LoadEquip()
        {
            DataTable dtTemp = null;
            try
            {
                dtTemp = DBHelper.Instance.GetDataTable("select *,EquipName+'('+EquipState+')' as LabelName from t_Equip");
            }
            catch
            {
                MessageBox.Show("加载数据失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            cs = mapControl1.Map.GetDisplayCoordSys();//获取地图坐标系
            MapInfo.Data.TableInfoMemTable tbEuqip = new MapInfo.Data.TableInfoMemTable("Equip");
            tbEuqip.Columns.Add(MapInfo.Data.ColumnFactory.CreateFeatureGeometryColumn(cs));//向表信息中添加必备的可绘图列
            tbEuqip.Columns.Add(MapInfo.Data.ColumnFactory.CreateStyleColumn());

            tbEuqip.Columns.Add(MapInfo.Data.ColumnFactory.CreateStringColumn("EquipID", 40)); //设备编码
            tbEuqip.Columns.Add(MapInfo.Data.ColumnFactory.CreateStringColumn("EquipName", 50)); //设备名称
            tbEuqip.Columns.Add(MapInfo.Data.ColumnFactory.CreateStringColumn("EquipState", 20)); //设备状态
            tbEuqip.Columns.Add(MapInfo.Data.ColumnFactory.CreateStringColumn("LabelName", 100)); //设备状态
            tbEuqip.Columns.Add(MapInfo.Data.ColumnFactory.CreateStringColumn("Remarks", 100)); //设备状态

            MapInfo.Data.Table table = MapInfo.Engine.Session.Current.Catalog.GetTable("Equip");//确保当前目录下不存在同名表
            if (table != null)
            {
                MapInfo.Engine.Session.Current.Catalog.CloseTable("Equip");
            }
            table = MapInfo.Engine.Session.Current.Catalog.CreateTable(tbEuqip);

            equipLayer = new FeatureLayer(table, "Equip", "Equip");//创建图层(并关联表)
            LabelSource source = new LabelSource(table);//绑定Table
            source.DefaultLabelProperties.Caption = "LabelName";//指定哪个字段作为显示标注
            source.DefaultLabelProperties.Style.Font.ForeColor = Color.Red;
            source.DefaultLabelProperties.CalloutLine.Use = false;  //是否使用标注线  
            source.DefaultLabelProperties.Layout.Offset = 5;//标注偏移   
            LabelLayer labelLayer = new LabelLayer();
            labelLayer.Sources.Append(source);//加载指定数据
            mapControl1.Map.Layers.Add(equipLayer);
            mapControl1.Map.Layers.Add(labelLayer);

            //LayerHelper.SetEditable(equipLayer, true);
            LayerHelper.SetSelectable(equipLayer, true);

            if (dtTemp == null || dtTemp.Rows.Count <= 0)
                return;

            MapTool.SetInfoTipExpression(mapControl1.Tools.MapToolProperties, equipLayer, "'设备状态：'+EquipState");

            NodeStyle = new MapInfo.Styles.BitmapPointStyle("LITE2-32.BMP", BitmapStyles.None, Color.Blue, 20);
            foreach (DataRow dr in dtTemp.Rows)
            {
                Feature NewEquipNode = new Feature(equipLayer.Table.TableInfo.Columns);
                NewEquipNode.Geometry = new MapInfo.Geometry.Point(cs, new DPoint(Convert.ToDouble(dr["PosX"]), Convert.ToDouble(dr["PosY"])));
                NewEquipNode["EquipID"] = dr["EquipID"];
                NewEquipNode["EquipName"] = dr["EquipName"];
                NewEquipNode["EquipState"] = dr["EquipState"];
                NewEquipNode["Remarks"] = dr["Remarks"];
                NewEquipNode["LabelName"] = dr["LabelName"];
                NewEquipNode.Style = NodeStyle;
                equipLayer.Table.InsertFeature(NewEquipNode);
            }
        }
        
        private void mapControl1_MouseDown(object sender, MouseEventArgs e)
        {
            System.Drawing.Point p = new System.Drawing.Point(e.X, e.Y);
            MapInfo.Geometry.DisplayTransform converter = mapControl1.Map.DisplayTransform;
            converter.FromDisplay(p, out DPLast);
        }

        private void tsmiAddEquip_Click(object sender, EventArgs e)
        {
            AddEquip frmAddEquip = new AddEquip();
            if (frmAddEquip.ShowDialog() == DialogResult.OK)
            {
                Feature NewEquipNode = new Feature(equipLayer.Table.TableInfo.Columns);
                NewEquipNode.Geometry = new MapInfo.Geometry.Point(cs, DPLast);
                NewEquipNode["EquipID"] = Guid.NewGuid().ToString();
                NewEquipNode["EquipName"] = frmAddEquip.EquipName;
                NewEquipNode["EquipState"] = frmAddEquip.EquipState;
                NewEquipNode["Remarks"] = frmAddEquip.Remarks;
                NewEquipNode["LabelName"] = frmAddEquip.EquipName+"("+frmAddEquip.EquipState+")";
                NewEquipNode.Style = NodeStyle;
                string sql = string.Format("insert into t_equip(EquipID,EquipName,EquipState,PosX,PosY,Remarks)values('{0}','{1}','{2}',{3},{4},'{5}')"
                    , NewEquipNode["EquipID"].ToString(), NewEquipNode["EquipName"].ToString(), NewEquipNode["EquipState"].ToString(), DPLast.x, DPLast.y,frmAddEquip.Remarks);
                if (DBHelper.Instance.ExcuteSql(sql))
                {
                    equipLayer.Table.InsertFeature(NewEquipNode);
                }
            }
        }

        void Tools_FeatureSelected(object sender, FeatureSelectedEventArgs e)
        {
            try
            {
                fSelectCollection = e.Selection[0];
            }
            catch
            {
                fSelectCollection = null;
            }
        }

        private void tsmiDelEquip_Click(object sender, EventArgs e)
        {
            if (fSelectCollection == null || fSelectCollection.Count <= 0)
            {
                MessageBox.Show("请选择设备进行删除！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int iCount = 0;
                foreach (Feature f in fSelectCollection)
                {
                    if (f.Table.Alias == "Equip_selection")
                    {
                        string EquipId = f["EquipID"].ToString();
                        if (DBHelper.Instance.ExcuteSql(String.Format("delete from t_equip where EquipID='{0}'", EquipId)))
                        {
                            f.Table.DeleteFeature(f);
                            iCount++;
                        }
                    }
                }
                if (iCount > 0)
                {
                    MessageBox.Show(string.Format("成功删除{0}个设备!",iCount), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("没有设备被删除!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}