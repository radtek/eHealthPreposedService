﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eHPS.WYServiceImplement;
using eHPS.Contract;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using eHPS.CrossCutting.NetFramework.ExceptionHandler;
using eHPS.CrossCutting.NetFramework.Logging;
using eHPS.CrossCutting.Logging;
using System.Collections.Generic;
using eHPS.Contract.Model;
using System.Linq;

using System.Dynamic;
using eHPS.Common;

using System.IO;

using System.Configuration;

namespace eHPS.ImplementTest
{
    [TestClass]
    public class BasicInfoTest
    {
        private IBasicInfo basicService;
        private UnityContainer container;

        [TestInitialize]
        public void Initialize()
        {
            container = new UnityContainer();

            container.RegisterType<IBasicInfo, BasicInfoService>( new Interceptor<InterfaceInterceptor>(), 
                    new InterceptionBehavior<LoggingInterceptionBehavior>());

            //设定日志类
            LoggerFactory.SetCurrent(new eHPSNLogFactory());



            container.AddNewExtension<Interception>();


        }


        [TestMethod]
        public void Verify_File_Copy()
        {
            var sourceUrl = "./Web.config";
            var destUrl = "./Web1.config";

            File.Copy(sourceUrl, destUrl);

            Assert.AreEqual(true,File.Exists(destUrl));
        }




        [TestMethod]
        public void Verify_ConfigHelper_Functional()
        {
            //eHPS.ImplementTest.dll.config
            var baseUrl = Environment.CurrentDirectory;
            var configUrl = baseUrl + @"\eHPS.ImplementTest.dll.config";
            var implementAssemblyUrl = baseUrl+ @"\eHPS.SomeImplement.dll";
            var contractAssemblyUrl = baseUrl + @"\eHPS.Contract.dll";
            var configFileUrl = "./Web.config";

            var impConfigUrl = baseUrl + @"\eHPS.SomeImplement.dll.config";

            var serviceUrl = "http://192.168.1.232/webservice/n_webservice.asmx";
            //var configFileUrl = baseUrl + "\\Web.config";
            var fileExits = File.Exists(configFileUrl);

            ConfigHelper.ConfigUnityConfig(configFileUrl, impConfigUrl,contractAssemblyUrl, implementAssemblyUrl);
        }




        [TestMethod]
        public void Get_All_Depts()
        {

            basicService = container.Resolve<IBasicInfo>();
            var areaId = "01";
            var result = basicService.GetDepts(areaId);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Is_Nlog_Functional()
        {
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

            logger.Error("what fuck");


        }

        private void ModifyRef(List<String> items)
        {
            items.Add("kobe");
            items.Add("jack");
        }

        [TestMethod]
        public void Wheather_Need_Ref()
        {
            var items = new List<String>();
            items.Add("michael");
            items.Add("jordan");

            ModifyRef(items);

            Assert.AreEqual(4, items.Count);


        }



        [TestMethod]
        public void Test_Dynamic()
        {
            dynamic obj = new ExpandoObject();

            obj.item1 = 1;
            obj.item2 = "1";



            Assert.AreEqual(1, Int32.Parse((String)obj.item2));

        }


        [TestMethod]
        public void Test_MathRound()
        {
            var para1 = 45.014;

            var roundPara1=Math.Round(para1, 2, MidpointRounding.AwayFromZero);

            Assert.AreEqual(45.01, roundPara1);
        }
        enum BindingProtocol
        {
            HTTP = 0,
            HTTPS = 1
        }

        [TestMethod]
        public void Display_Enum()
        {
            Assert.AreEqual("HTTP", BindingProtocol.HTTP.ToString());
        }



        [TestMethod]
        public void Test_List()
        {
            List<OrderItem> orders = new List<OrderItem>
            {
                new OrderItem {
                     ItemId="1"
                },
                new OrderItem {
                     ItemId="2"
                },
                new OrderItem {
                     ItemId="3"
                },
                new OrderItem {
                     ItemId="4"
                }


            };


            var item = orders.FirstOrDefault(p => p.ItemId == "1");

            item.ItemName = "Damn";


            Assert.AreEqual("Damn", orders.FirstOrDefault(p => p.ItemId == "1").ItemName);





        }



        [TestMethod]
        public void Verify_ServieControl_GetService_Avaliable()
        {
            var result =
            DeployHelper.IsServiceExist("PushToPayService");


            Assert.AreEqual(true,result);
        }






    }
}
