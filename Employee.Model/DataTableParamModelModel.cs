using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Model
{
    public class DataTableParamModelModel
    {
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
        public int? iColumns { get; set; }  // null by me
        public string sSearch { get; set; }
        //public int sSearchInt { get; set; }       // by me
        public bool bEscapeRegex { get; set; }
        public int? iSortingCols { get; set; }       // null by me
        public int sEcho { get; set; }

        //by me new

        public int iSortCol_0 { get; set; }
        public string sSortDir_0 { get; set; }

        //by me new



        //public List<string> sColumnNames { get; set; }
        //public List<bool> bSortable { get; set; }
        //public List<bool> bSearchable { get; set; }
        //public List<string> sSearchValues { get; set; }
        //public List<int> iSortCol { get; set; }
        //public List<string> sSortDir { get; set; }
        //public List<bool> bEscapaeRegexColumns { get; set; }


        //public DataTableParamModel() 
        //{
        //    sColumnNames = new List<string>();
        //    bSortable = new List<bool>();
        //    bSearchable = new List<bool>();
        //    sSearchValues = new List<string>();
        //    iSortCol = new List<int>();
        //    sSortDir = new List<string>();
        //    bEscapaeRegexColumns = new List<bool>();
        //}

        //public DataTableParamModel(int iColumns)
        //{
        //    this.iColumns = iColumns;
        //    sColumnNames = new List<string> (iColumns);
        //    bSortable = new List<bool>(iColumns);
        //    bSearchable = new List<bool>(iColumns);
        //    sSearchValues = new List<string>(iColumns);
        //    iSortCol = new List<int>(iColumns);
        //    sSortDir = new List<string>(iColumns);
        //    bEscapaeRegexColumns = new List<bool>(iColumns);
        //}
    }
}
