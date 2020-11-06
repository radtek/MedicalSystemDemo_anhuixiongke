CREATE VIEW V_OPER_PROGRESS_INFO AS
SELECT
TOP 100 percent
--手术间（复苏床位）名称
A.BED_LABEL,
--手术间（复苏床位）号
A.ROOM_NO,
--排序字段
A.BED_ID,
--手术科室代码
A.DEPT_CODE OPER_DEPT_CODE,
--手术科室名称
ISNULL((SELECT V_DEPT_DICT.DEPT_NAME FROM V_DEPT_DICT WHERE V_DEPT_DICT.DEPT_CODE = B.OPERATING_DEPT),B.OPERATING_DEPT) OPER_DEPT_NAME,
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
DATEDIFF(YEAR,GETDATE(),C.DATE_OF_BIRTH) AS PAT_AGE,
--急诊择期标记
B.EMERGENCY_IND,
--隔离标记
B.ISOLATION_IND,
--放射标记
B.RADIATE_IND,
--感染标记
B.INFECTED_IND,
--住院科室名
ISNULL((SELECT V_DEPT_DICT.DEPT_NAME FROM V_DEPT_DICT WHERE V_DEPT_DICT.DEPT_CODE = B.DEPT_STAYED),B.DEPT_STAYED) DEPT_STAYED_NAME,
--手术名称
B.OPERATION_NAME,
--手术等级
B.OPERATION_SCALE,
--麻醉方法
B.ANESTHESIA_METHOD,
 --手术者
ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = B.SURGEON ),B.SURGEON ) SURGEON,
--第一手术助手
ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = B.FIRST_ASSISTANT ),B.FIRST_ASSISTANT ) FIRST_ASSISTANT,
--麻醉医生
ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = B.ANESTHESIA_DOCTOR ),B.ANESTHESIA_DOCTOR ) ANESTHESIA_DOCTOR,
--副麻医生
ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = B.ANESTHESIA_ASSISTANT ),B.ANESTHESIA_ASSISTANT ) ANESTHESIA_ASSISTANT,
 --第一洗手护士
ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = B.FIRST_OPERATION_NURSE ),B.FIRST_OPERATION_NURSE ) FIRST_OPERATION_NURSE,
--第一巡回护士
ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = B.FIRST_SUPPLY_NURSE ),B.FIRST_SUPPLY_NURSE ) FIRST_SUPPLY_NURSE,
--手术间或复苏室
CASE A.BED_TYPE WHEN '0' THEN '手术间' WHEN '1' THEN '复苏室' END AS ROOM_TYPE,
--手术状态代码
B.OPER_STATUS AS OPER_STATE_CODE,
--手术状态名
CASE B.OPER_STATUS WHEN 5 THEN '入手术室' WHEN 10 THEN '麻醉开始' WHEN 15 THEN '手术开始' WHEN 25 THEN '手术结束' WHEN 30 THEN '麻醉结束' WHEN 45 THEN '入复苏室' END AS OPER_STATE,
--当前状态时间
CASE B.OPER_STATUS WHEN 5 THEN CONVERT(CHAR(5),B.IN_DATE_TIME,108) WHEN 10 THEN CONVERT(CHAR(5),B.ANES_START_TIME,108) WHEN 15 THEN CONVERT(CHAR(5),B.START_DATE_TIME,108) WHEN 25 THEN CONVERT(CHAR(5),B.END_DATE_TIME,108) WHEN 30 THEN CONVERT(CHAR(5),B.ANES_END_TIME,108) WHEN 45 THEN CONVERT(CHAR(5),B.IN_PACU_DATE_TIME,108) END AS PRO_TIME
FROM V_OPERATING_ROOM A
LEFT OUTER JOIN V_OPERATION_MASTER B ON A.PATIENT_ID = B.PATIENT_ID AND A.VISIT_ID = B.VISIT_ID AND A.OPER_ID = B.OPER_ID
LEFT OUTER JOIN V_PAT_MASTER_INDEX C ON A.PATIENT_ID = C.PATIENT_ID
ORDER BY A.BED_TYPE,A.BED_LABEL
;
