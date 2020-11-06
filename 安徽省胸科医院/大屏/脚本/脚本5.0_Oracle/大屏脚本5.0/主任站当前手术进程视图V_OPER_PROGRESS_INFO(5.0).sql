CREATE OR REPLACE VIEW V_OPER_PROGRESS_INFO AS
SELECT
--手术间（复苏床位）名称
A.BED_LABEL,
--手术间（复苏床位）号
A.ROOM_NO,
--排序字段
A.BED_ID,
--手术科室代码
A.DEPT_CODE OPER_DEPT_CODE,
--手术科室名称
NVL((SELECT V_DEPT_DICT.DEPT_NAME FROM V_DEPT_DICT WHERE V_DEPT_DICT.DEPT_CODE = B.OPERATING_DEPT),B.OPERATING_DEPT) OPER_DEPT_NAME,
--患者3个ID和住院号
A.PATIENT_ID,C.INP_NO,A.VISIT_ID,A.OPER_ID,
--手术日期
B.SCHEDULED_DATE_TIME,
--入手术室时间
B.IN_DATE_TIME,
--手术开始时间
B.START_DATE_TIME,
--患者姓名
C.NAME PAT_NAME,
--性别
C.SEX,
--年龄
DECODE(TO_CHAR(SYSDATE,'YYYY') - TO_CHAR(C.DATE_OF_BIRTH,'YYYY'),NULL,'',TO_CHAR(SYSDATE,'YYYY') - TO_CHAR(C.DATE_OF_BIRTH,'YYYY') || '岁') AS PAT_AGE,
--急诊择期标记
B.EMERGENCY_IND,
--隔离标记
B.ISOLATION_IND,
--放射标记
B.RADIATE_IND,
--感染标记
B.INFECTED_IND,
--住院科室名
NVL((SELECT V_DEPT_DICT.DEPT_NAME FROM V_DEPT_DICT WHERE V_DEPT_DICT.DEPT_CODE = B.DEPT_STAYED),B.DEPT_STAYED) DEPT_STAYED_NAME,
--手术名称
B.OPERATION_NAME,
--手术等级
B.OPERATION_SCALE,
--麻醉方法
B.ANESTHESIA_METHOD,
 --手术者
NVL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = B.SURGEON ),B.SURGEON ) SURGEON,
--第一手术助手
NVL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = B.FIRST_ASSISTANT ),B.FIRST_ASSISTANT ) FIRST_ASSISTANT,
--麻醉医生
NVL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = B.ANESTHESIA_DOCTOR ),B.ANESTHESIA_DOCTOR ) ANESTHESIA_DOCTOR,
--副麻医生
NVL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = B.ANESTHESIA_ASSISTANT ),B.ANESTHESIA_ASSISTANT ) ANESTHESIA_ASSISTANT,
 --第一洗手护士
NVL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = B.FIRST_OPERATION_NURSE ),B.FIRST_OPERATION_NURSE ) FIRST_OPERATION_NURSE,
--第一巡回护士
NVL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = B.FIRST_SUPPLY_NURSE ),B.FIRST_SUPPLY_NURSE ) FIRST_SUPPLY_NURSE,
--手术间或复苏室
CASE A.BED_TYPE WHEN '0' THEN '手术间' WHEN '1' THEN '复苏室' END AS ROOM_TYPE,
--手术状态代码
B.OPER_STATUS AS OPER_STATE_CODE,
--手术状态名
DECODE(B.OPER_STATUS, 5,'入手术室',10,'麻醉开始',15,'手术开始',25,'手术结束',30,'麻醉结束',45,'入复苏室') OPER_STATE,
--当前状态时间
DECODE(B.OPER_STATUS, 5,TO_CHAR(B.IN_DATE_TIME,'HH24:mm'),10,TO_CHAR(B.ANES_START_TIME,'HH24:mm'),15,TO_CHAR(B.START_DATE_TIME,'HH24:mm'),25,TO_CHAR(B.END_DATE_TIME,'HH24:mm'),30,TO_CHAR(B.ANES_END_TIME,'HH24:mm'),45,TO_CHAR(B.IN_PACU_DATE_TIME,'HH24:mm')) PRO_TIME
FROM V_OPERATING_ROOM A
LEFT OUTER JOIN V_OPERATION_MASTER B ON A.PATIENT_ID = B.PATIENT_ID AND A.VISIT_ID = B.VISIT_ID AND A.OPER_ID = B.OPER_ID
LEFT OUTER JOIN V_PAT_MASTER_INDEX C ON A.PATIENT_ID = C.PATIENT_ID
ORDER BY A.BED_TYPE,A.BED_LABEL;

-- Grant/Revoke synonym 
CREATE OR REPLACE PUBLIC SYNONYM V_OPER_PROGRESS_INFO FOR V_OPER_PROGRESS_INFO;
