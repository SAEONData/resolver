using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace resolver
{
    public partial class get : System.Web.UI.Page
    {
        public String view = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            String guid = Request["guid"];
            String query = "SELECT * FROM TblDOI WHERE fGUID = '" + guid.Replace("'", "''") + "'";
            using (SqlConnection con = new SqlConnection(species.DataSources.dbConResolver))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader set = cmd.ExecuteReader())
                    {
                        if (set.Read())
                        {
                            view = set["fView"].ToString();
                        }
                    }
                }
            }
        }
    }
}