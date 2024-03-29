﻿//===================================================================================
// 北京联想智慧医疗信息技术有限公司 & 上海研发中心
//===================================================================================
// 诊疗服务，包含：
// 获取用户诊疗记录，发起在线诊疗
//
//===================================================================================
// .Net Framework 4.5
// CLR版本： 4.0.30319.42000
// 创建人：  Jay
// 创建时间：2016/6/24 16:51:43
// 版本号：  V1.0.0.0
//===================================================================================

using eHPS.API.Models;
using eHPS.Contract;
using eHPS.Contract.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace eHPS.API.Controllers
{
    /// <summary>
    /// 诊疗服务，包含：
    /// 获取用户诊疗记录，发起在线诊疗
    /// </summary>
    [RoutePrefix("Diagnosis")]//,Authorize
    public class DiagnosisController : ApiController
    {

        private IDiagnosis diagnosisService;

        public DiagnosisController(IDiagnosis diagnosisService)
        {
            this.diagnosisService = diagnosisService;
        }


        /// <summary>
        /// 根据用户标识，获取用户诊疗记录
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [Route("DiagnosisHistory"),HttpPost, ResponseType(typeof(List<DiagnosisRecord>))]
        public List<DiagnosisRecord> GetDiagnosisHistory([FromBody]String patientId)
        {
            return diagnosisService.GetDiagnosisHistory(patientId);
        }



        /// <summary>
        /// 发起在线诊疗
        /// 操作挂号信息
        /// 挂号收费后置
        /// </summary>
        /// <param name="request">发起在线诊疗请求包装对象</param>
        /// <returns>
        /// HasError = 0 : 在线诊疗发起成功
        /// HasError = 1 : 在线诊疗发起失败
        /// </returns>
        [Route("MakeADiagnosis"), HttpPost, ResponseType(typeof(ResponseMessage<string>))]
        public ResponseMessage<string> MakeADiagnosis([FromBody]MakeADiagnosisRequest request)
        {
            return diagnosisService.MakeADiagnosis(request.PatientId, request.PId,request.DoctorId,request.DeptId,request.Complaint);
        }
    }
}
