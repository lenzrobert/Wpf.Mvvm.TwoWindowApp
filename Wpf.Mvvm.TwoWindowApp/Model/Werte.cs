using System.Data;

namespace Wpf.Mvvm.TwoWindowApp.Model
{
    public class Werte
    {
        public string WertListBox { get; set; }
        public string WertComboBox { get; set; }
        public string WertDataSet { get; set; }
        public bool ValueCheck { get; set; }
        DataSet _ds = new DataSet("Neu");

        public DataSet GetListeArtikel()
        {
            DataTable dtArtikel = new DataTable("Artikel");
            dtArtikel.Columns.Add("Nr", typeof(string));
            dtArtikel.Columns.Add("Name", typeof(string));
            _ds.Tables.Add(dtArtikel);

            AddNewRowToTable("0815", "Normalfall");
            AddNewRowToTable("0816", "Panther");
            AddNewRowToTable("0618", "Baer");
            AddNewRowToTable("0811", "Dachs");
            AddNewRowToTable("a815", "Marder");
            AddNewRowToTable("b815", "Fuchs");
            return _ds;
        }

        private void AddNewRowToTable(string nrValue, string artikelValue)
        {
            DataRow dr;
            dr = _ds.Tables["Artikel"].NewRow();
            dr["Nr"] = nrValue;
            dr["Name"] = artikelValue;
            _ds.Tables["Artikel"].Rows.Add(dr);
        }
    }
}
