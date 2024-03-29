﻿//===================================================================================
// 北京联想智慧医疗信息技术有限公司 & 上海研发中心
//===================================================================================
// 基础信息服务，温附一HIS系统实现
//
//
//===================================================================================
// .Net Framework 4.5
// CLR版本： 4.0.30319.42000
// 创建人：  Jay
// 创建时间：2016/6/29 11:14:54
// 版本号：  V1.0.0.0
//===================================================================================




using eHPS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

using Dapper;
using eHPS.Contract.Model;
using eHPS.CrossCutting.NetFramework.Caching;

namespace eHPS.WYServiceImplement
{
    public class BasicInfoService : IBasicInfo
    {
        public List<Organization> GetDepts(string areaId)
        {
            var depts = new List<Organization>();
            using (var con = DapperFactory.CrateOracleConnection())
            {
                var command = @"SELECT BMID ,BMMC  FROM XTGL_BMDM WHERE SJBM=1 AND YQDM=:AreaId";
                var condition = new { AreaId=areaId };
                var parent = con.Query(command, condition).ToList();
                foreach (var dept in parent)
                {
                    var organization = new Organization
                    {
                        ParentDept= new Department
                        {
                            DeptId = (Int32)dept.BMID + "",
                            DeptName = (String)dept.BMMC
                        },
                        Subdivision = new List<Department>()
                    };

                    var subCommand = @"SELECT BMID ,BMMC  FROM XTGL_BMDM WHERE SJBM="+(Int32)dept.BMID+" AND YQDM=:AreaId";
                    var subDepts = con.Query(subCommand, condition).ToList();
                    foreach (var subDept in subDepts)
                    {
                        organization.Subdivision.Add(new Department {
                                DeptId= (Int32)subDept.BMID + "",
                                DeptName=(String)subDept.BMMC
                        });
                    }
                    depts.Add(organization);
                }


            }

            return depts;
           
        }

        /// <summary>
        /// 获取科室名称
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public string GetDeptName(string deptId)
        {
            using (var con = DapperFactory.CrateOracleConnection())
            {
                var command = @"SELECT BMID AS DeptId,BMMC AS DeptName,' ' AS ParentDeptId  FROM XTGL_BMDM WHERE BMID=:DeptId ";
                var condition = new { DeptId=Int32.Parse(deptId) };
                var dept =con.Query<Department>(command,condition).FirstOrDefault();
                if(null!= dept)
                {
                    return dept.DeptName;
                }
                else
                {
                    return "";
                }
            }
        }


        /// <summary>
        /// 根据用户标识获取用户姓名
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public string GetNameById(string userId)
        //{
        //    using (var con = DapperFactory.CrateOracleConnection())
        //    {
        //        var command = @"select yhxm from xtgl_yhxx where yhid =:UserId";

        //        var condition = new { UserId = userId };

        //        var result = con.Query(command, condition).FirstOrDefault();

        //        return (string)result.yhxm;
        //    }
        //}



        public Doctor GetDoctorById(string doctorId)
        {
            var doctor = default(Doctor);
            using (var con = DapperFactory.CrateOracleConnection())
            {
                var command = @"SELECT A.YHID, A.RYKID,A.XM,A.XB,A.GZDM,B.JSNR,B.PIC,A.XZKID FROM YL_RYK A LEFT JOIN YYFZ_YSJS B ON A.RYKID=B.RYKID WHERE  A.ZTBZ=1 AND A.YHID=:DoctorId";
                var condition = new { DoctorId = Int32.Parse(doctorId) };
                var result = con.Query(command, condition).FirstOrDefault();
                if (result!=null)
                {
                    doctor = new Doctor
                    {
                        DeptId = (Int32)result.XZKID+"",
                        DeptName = CommonService.GetDeptName((Int32)result.XZKID),
                        DoctorId = ((Int64)result.YHID).ToString(),
                        DoctorName = (string)result.XM,
                        Expert = (string)result.JSNR,
                        Introduction = (string)result.JSNR,
                        JobTitle = CommonService.GetJobTitle((string)result.GZDM),
                        Sex = (string)result.XB == "1" ? "男" : "女",
                        Photo = (byte[])result.PIC
                    };
                    
                }

            }

            return doctor;
        }


        /// <summary>
        /// 医生标识
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public List<Doctor> GetDoctors(string deptId)
        {
            var doctors = new List<Doctor>();
            using (var con = DapperFactory.CrateOracleConnection())
            {
                var command = @"SELECT A.YHID, A.RYKID,A.XM,A.XB,A.GZDM,B.JSNR,B.PIC FROM YL_RYK A LEFT JOIN YYFZ_YSJS B 
                                            ON A.RYKID=B.RYKID WHERE  A.ZTBZ=1 AND A.XZKID=:DeptId";
                var condition = new { DeptId=deptId };
                var result = con.Query(command, condition).ToList();
                foreach (var item in result)
                {
                    var doctor = new Doctor {
                         DeptId=deptId,
                         DeptName=CommonService.GetDeptName(Int32.Parse(deptId)),
                         DoctorId=((Int64)item.YHID).ToString(),
                         DoctorName=(string)item.XM,
                         Expert= (string)item.JSNR,
                         Introduction=(string)item.JSNR,
                         JobTitle=CommonService.GetJobTitle((string)item.GZDM),
                         Sex=(string)item.XB=="1"?"男":"女",
                         Photo=(byte[])item.PIC
                    };
                    doctors.Add(doctor);
                }

            }
            return doctors;
        }



        /// <summary>
        /// 根据患者标识获取患者基本信息
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Patient GetPatientInfo(string patientId)
        {

            using (var con = DapperFactory.CrateOracleConnection())
            {
                var command = @"SELECT BRBH,DWDM,KNSJ,BRXM,BRXB,GJDM,HYZK,ZYDM,JGDM,
                                            MZDM,SFZH,CSRQ,XZZDM,LXDZ,LXDH,YDDH,SZSJ,ZTBZ FROM CW_KHXX WHERE BRBH = :PatientId";
                var condition = new { PatientId =patientId};

                var result = con.Query(command, condition).FirstOrDefault();

                if(result!=null)
                {
                    var patient = new Patient {
                            PatientId =(string)result.BRBH,
                            PatientName=(string)result.BRXM,
                            BornDate=(DateTime)result.CSRQ,
                            ContactAddress=(string)result.LXDZ,
                            IdCode=(string)result.SFZH,
                            Mobile=(string)result.YDDH,
                            Sex=(string)result.BRXB == "1" ? "男" : "女",
                            Telephone =(string)result.LXDH,
                            FirstTimeDiagnosis=(DateTime)result.SZSJ
                    };
                    return patient;
                }
                else
                {
                    return new Patient();
                }

           
            }
        }


        /// <summary>
        /// 获取挂号费用
        /// </summary>
        /// <param name="diagnosisType">诊疗类型</param>
        /// <param name="jobTitle">挂牌工种</param>
        public  decimal GetRegisteredAmount(string diagnosisTypeId, string jobTitleId)
        {
            using (var con = DapperFactory.CrateOracleConnection())
            {
                var command = @"select zllx,gzdm,xmid,xmje from cw_zllxghxm";
                //var result = new Tuple<String,String,Int32,decimal>()
                var result = con.Query(command).ToList();

                var amount = default(decimal);
                foreach (var item in result)
                {
                    if ((string)item.ZLLX == diagnosisTypeId && (string)item.GZDM == jobTitleId)
                    {
                        amount += (decimal)item.XMJE;
                    }
                }

                return amount;

            }
        }


        /// <summary>
        /// 根据患者注册的手机号码获取患者基本信息
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns></returns>
        public List<Patient> GetPatientInfoByMobile(string mobile)
        {
            var patients = new List<Patient>();
            using (var con = DapperFactory.CrateOracleConnection())
            {
                var command = @"SELECT BRBH,DWDM,KNSJ,BRXM,BRXB,GJDM,HYZK,ZYDM,JGDM,
                                            MZDM,SFZH,CSRQ,XZZDM,LXDZ,LXDH,YDDH,SZSJ,ZTBZ FROM CW_KHXX WHERE YDDH = :Mobile";
                var condition = new { Mobile = mobile };

                var result = con.Query(command, condition).ToList();

                foreach (var item in result)
                {
                    var patient = new Patient
                    {
                        PatientId = (string)item.BRBH,
                        PatientName = (string)item.BRXM,
                        BornDate = (DateTime)item.CSRQ,
                        ContactAddress = (string)item.LXDZ,
                        IdCode = (string)item.SFZH,
                        Mobile = (string)item.YDDH,
                        Sex = (string)item.BRXB == "1" ? "男" : "女",
                        Telephone = (string)item.LXDH,
                        FirstTimeDiagnosis = (DateTime)item.SZSJ
                    };

                    patients.Add(patient);

                }

                return patients;

            }
        }




        /// <summary>
        /// 根据姓名或者拼音查询医生信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="spelling">拼音</param>
        /// <returns></returns>
        public List<Doctor> GetDoctors(string name, string spelling)
        {
            var doctors = new List<Doctor>();
            using (var con = DapperFactory.CrateOracleConnection())
            {
                var command = @"SELECT A.YHID, A.RYKID,A.XM,A.XB,A.GZDM,A.XZKID,B.JSNR,B.PIC FROM YL_RYK A LEFT JOIN YYFZ_YSJS B ON A.RYKID=B.RYKID 
                                            WHERE  A.ZTBZ=1 AND (A.XM LIKE '%"+name+"%' OR A.PY LIKE '%"+ spelling + "%')";
                
                var result = con.Query(command).ToList();
                foreach (var item in result)
                {
                    String deptId = String.Empty;
                    String deptName = String.Empty;
                    //现专科ID可能为空
                    if(item.XZKID!=null)
                    {
                        deptId = (Int32)item.XZKID + "";
                        deptName = CommonService.GetDeptName((Int32)item.XZKID);
                    }
                    
                    var doctor = new Doctor
                    {
                        DeptId = deptId,
                        DeptName = deptName,
                        DoctorId = ((Int64)item.YHID).ToString(),
                        DoctorName = (string)item.XM,
                        Expert = (string)item.JSNR,
                        Introduction = (string)item.JSNR,
                        JobTitle = CommonService.GetJobTitle((string)item.GZDM),
                        Sex = (string)item.XB == "1" ? "男" : "女",
                        Photo = (byte[])item.PIC
                    };
                    doctors.Add(doctor);
                }

            }
            return doctors;
        }
    }
}
