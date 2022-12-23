using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InterfaceSingTaxitoSQL.Context;
using InterfaceSingTaxitoSQL.Model;
using System.Net;
using System.Data;
using SingTaxiAPI.Model;
using Newtonsoft.Json;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint;
using System.Security;
using System.Xml;
using RestSharp;
using System.Data.Entity.Infrastructure;
using System.Runtime.Remoting.Contexts;
using Microsoft.Office.Server.Auditing;
using System.Threading;
using Microsoft.Office.Server.Search.Query;

namespace InterfaceSingTaxitoSQL.Class
{
    public class SynchronizetoSingTaxiAPI
    {
        //initiate data
        DataContext context = new DataContext();
        public void SynchronizeSingTaxiResultToStaging(string url)
        {
            try
            {
                var allTaxi = 1;
                var result = new RootObject();

                var client = new RestClient(url);
                var request = new RestRequest("transport/taxi-availability", Method.Get);
                var response = client.Execute(request);
                

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string rawResponse = response.Content;
                    result = JsonConvert.DeserializeObject<RootObject>(rawResponse);

                    allTaxi = result.features[0].geometry.coordinates.Count;
                }

                for (int i = 0; i < allTaxi; i++)
                {
                    try 
                    {
                        var client1 = new RestClient("https://maps.googleapis.com/maps/api/geocode/");
                        var request1 = new RestRequest("json?key=AIzaSyBmBHPXZele7tExGXVwfg8yvRuK8md1RiU");

                        var lon = (result.features[0].geometry.coordinates[i][0]).ToString("0.000000").Replace(',', '.');
                        var lat = (result.features[0].geometry.coordinates[i][1]).ToString("0.000000").Replace(',', '.');

                        //string test2 = lon.ToString("0.000000").Replace(',', '.');
                        //string test1 = lat.ToString("0.000000").Replace(',', '.');

                        request1.AddParameter("latlng", lat + ',' + lon);
                        request1.AddParameter("result_type", "neighborhood");


                        var response1 = client1.Execute(request1);

                        string rawResponse1 = response1.Content;
                        var result1 = JsonConvert.DeserializeObject<Root>(rawResponse1);

                        var wOut = new TaxiHistory();
                        wOut.lat = lat;
                        wOut.lon = lon;
                        if (result1.status == "OK")
                        {
                            wOut.suburb = (result1.results[0].formatted_address).Split(',')[0];
                        }
                        else
                        {
                            wOut.suburb = "";
                        }
                        wOut.status = result.features[0].properties.api_info.status;
                        wOut.last_updated_time = result.features[0].properties.timestamp;

                        context.Entry(wOut).State = EntityState.Added;

                        context.Configuration.AutoDetectChangesEnabled = false;
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                    }

                }

                context.SaveChanges();

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
    }
}
