using ChandlerPro.Controllers;
using ChandlerPro.Models;
using ChandlerPro.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChandlerPro
{
    class Program
    {

        static async Task Main(string[] args)
        {
            string location = ConfigurationManager.AppSettings["Location"];
            string userID = ConfigurationManager.AppSettings["userID"];
            //Models.Location location = await Controllers.LocationController.UseLocation();
            RootPOHeader objRootPOHeader = Utils.EpicorConnection.getPOs();
            lmblMinPOHeader objlmblMinPOHeader = new lmblMinPOHeader();
            objlmblMinPOHeader.locationID = Convert.ToInt32(location);
            objlmblMinPOHeader.userID = Convert.ToInt32(userID);
            RootPODtl objRootPODtl = Utils.EpicorConnection.getPODetails();  //SP
            RootPORel objRootPORel = Utils.EpicorConnection.getPORels(); //SP
            foreach (POHeader item in objRootPOHeader.value)
            {
                objlmblMinPOHeader.date = Convert.ToInt32(UnixTimestamp(Convert.ToDateTime(item.OrderDate)));
                objlmblMinPOHeader.requestDescription = "#EPOBUYID-"+item.BuyerID+",#EPOAPPRV-"+item.Approve+",#EPO-"+item.PONum;
                Task<int> vendorID = VendorController.getVendorID(item.VendorVendorID);
                if (vendorID == null || await vendorID == 0)
                {
                    VendorVM objVendor = new VendorVM
                    {
                        locationID = location,
                        name = item.VendorName,
                        email = item.VendorCntEmailAddress,
                        contact=item.VendorPPPrimPCon,
                        phone=item.VendorCntPhoneNum
                    };
                    vendorID = VendorController.NewVendorID(objVendor, item.VendorVendorID);
                }
                objlmblMinPOHeader.vendorID = await vendorID;
                Task<int> PurchaseOrderID = PurchaseOrderController.NewPurchaseOrder(objlmblMinPOHeader);
                if( await PurchaseOrderID == -999)
                {
                    Console.WriteLine("NewPurchaseOrder failed " + await PurchaseOrderID);
                    continue;
                }
                //Console.WriteLine("NewPurchaseOrder created " + await PurchaseOrderID);
                //PO -- PONum 100

                List<PODtl> objRootPODtlflter = objRootPODtl.value.FindAll(poitem => poitem.PONUM == item.PONum); //SP

                //objRootPODtlflter PO details which are belogs to 100

                foreach (var epicPODetailItem in objRootPODtlflter)
                {

                   Task<int> partID = PartController.getPartID(epicPODetailItem.PartNum);
                    if (partID == null || await partID == 0)
                    {
                        PartVM objPart = new PartVM
                        {
                            locationID = location,
                            name = epicPODetailItem.PartNum
                        };
                        partID = PartController.NewPartID(objPart, epicPODetailItem.PartNum);
                    }

                    lmblPODtl objLblItems = new lmblPODtl();
                    objLblItems.description = epicPODetailItem.LineDesc;
                    //  string orderQuantity =  String.Format("{0:0.####}",Convert.ToDouble(epicPODetailItem.OrderQty));
                    string orderQuantity = ReClasser.FormateQty(epicPODetailItem.OrderQty);
                    objLblItems.quantity =Convert.ToInt32(orderQuantity);
                    objLblItems.tax = float.Parse(epicPODetailItem.TotalTax);
                    objLblItems.shipping = 0.00;
                    objLblItems.name = epicPODetailItem.LineDesc;
                    objLblItems.discount = 0;
                    objLblItems.rate = float.Parse(epicPODetailItem.UnitCost);
                    objLblItems.partID = await partID;//LimblePartNumber
                    objLblItems.itemType = 1;
                    Task<int> POItemID = POItemController.NewPOItem(objLblItems,await PurchaseOrderID);
                    if (await POItemID == -999)
                    {
                        Console.WriteLine("NewPOItem failed " + await POItemID);
                        continue;
                    }
                    //Console.WriteLine("NewPOItem created " + await POItemID);

                }


                List<PORel> objRootPORelflter = objRootPORel.value.FindAll(relitem => relitem.PONum == item.PONum); //SP

            }
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("============== End ======================");
            Console.WriteLine("-----------------------------------------");
        }

        public static string UnixTimestamp(DateTime dateTime)
        {
            DateTimeOffset dto = new DateTimeOffset(dateTime);
            return dto.ToUnixTimeSeconds().ToString();
        }



    }
}
