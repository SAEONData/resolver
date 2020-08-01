using portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace resolver
{
    public partial class regdoi : System.Web.UI.Page
    {
        String resolveURL = "http://app01.saeon.ac.za/get.aspx?guid=";


        public int GetDOIID(SqlConnection con, String doi)
        {
            SuperSQL sql = new SuperSQL(con, null, Response);
            sql.add("fDOI", doi);
            object id = sql.select("TblDOI", "fDOIID");
            return id == null ? 0 : (int)id;
        }

        protected string AddDOIToDB(String doi, String user, String portal, String xml, String view)
        {
            string guid = "";

            //using (SqlConnection con = new SqlConnection(species.DataSources.dbConResolver))
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["Database"]))
            {
                con.Open();

                int id = GetDOIID(con, doi);


                SuperSQL sql = new SuperSQL(con, null, Response);
                sql.add("fDOI", doi);
                sql.add("fUserName", user);
                sql.add("fPortalName", portal);
                sql.add("fXML", xml);
                sql.add("fView", view);

                if (id == 0)
                {
                    Response.Write("add doi<br>");
                    sql.insert("TblDOI");
                    id = GetDOIID(con, doi);
                }
                else
                {
                    Response.Write("edit doi<br>");
                    sql.where("fDOIID", id);
                    sql.edit("TblDOI");
                }

                sql.clear();
                sql.add("fDOIID", id);
                guid = sql.select("TblDOI", "fGUID").ToString();
            }

            return guid;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            String data = Request["data"];
            String view = Request["view"];
            String doi = Request["doi"];

            try
            {
                json2datacite j2d = new json2datacite(Context);
                String xml = j2d.FromURL(data, doi);

                String guid = AddDOIToDB(doi, "Z10", "oa", xml, view);
                String url = resolveURL + Server.HtmlEncode(guid);

                String username = "NRF.TEST";
                String password = "D@taC!te";
                String content = "";

                DataCiteMetadataREST client = new DataCiteMetadataREST(username, password, "https://mds.test.datacite.org");
                Response.Write("url: " + client.Uri + "<br>");
                Response.Write("username: " + username + "<br>");
                Response.Write("password: " + password + "<br>");
                Response.Write("<br>");

                // post metadata
                Response.Write("***** Post metadata *******<br>");
                content = client.setMetadata(xml);
                Response.Write(content);
                Response.Write("<br><br>");

                // set doi
                Response.Write("***** Update DOI URL *******<br>");
                client.setDoi(doi, url);
                Response.Write(content);
                Response.Write("<br><br>");

            }
            catch (Exception err)
            {
                Response.Write(err.Message);
                Response.End();
            }


        }

        
    }
}