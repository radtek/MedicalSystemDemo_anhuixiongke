CREATE OR REPLACE VIEW V_OPER_ROOM_INFOS AS
SELECT
--手术间（复苏床位）名称
A.OPERATING_ROOM_NO BED_LABEL,
--手术间（复苏床位）号
A.OPERATING_ROOM_NO,
--台次
A.SEQUENCE,
--手术科室代码
A.OPERATING_ROOM OPER_DEPT_CODE,
--手术科室名称
NVL((SELECT V_DEPT_DICT.DEPT_NAME FROM V_DEPT_DICT WHERE V_DEPT_DICT.DEPT_CODE = A.OPERATING_DEPT),A.OPERATING_DEPT) OPER_DEPT_NAME,
--患者3个ID和住院号
A.PATIENT_ID,D.INP_NO,A.VISIT_ID,A.OPER_ID,
--手术日期
A.SCHEDULED_DATE_TIME,
--入手术室时间
A.IN_DATE_TIME,
--手术开始时间
A.START_DATE_TIME,
--患者姓名
C.NAME PAT_NAME,
--性别
C.SEX,
--年龄
DECODE(TO_CHAR(SYSDATE,'YYYY') - TO_CHAR(C.DATE_OF_BIRTH,'YYYY'),NULL,'',TO_CHAR(SYSDATE,'YYYY') - TO_CHAR(C.DATE_OF_BIRTH,'YYYY') || '岁') AS PAT_AGE,
--急诊择期标记
A.EMERGENCY_IND,
--隔离标记
A.ISOLATION_IND,
--放射标记
A.RADIATE_IND,
--感染标记
A.INFECTED_IND,
--住院科室名
NVL((SELECT V_DEPT_DICT.DEPT_NAME FROM V_DEPT_DICT WHERE V_DEPT_DICT.DEPT_CODE = A.DEPT_STAYED),A.DEPT_STAYED) DEPT_STAYED_NAME,
--手术名称
A.OPERATION_NAME,
--手术等级
A.OPERATION_SCALE,
--麻醉方法
A.ANESTHESIA_METHOD,
 --手术者
NVL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.SURGEON ),A.SURGEON ) SURGEON,
--第一手术助手
NVL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FIRST_ASSISTANT ),A.FIRST_ASSISTANT ) FIRST_ASSISTANT,
--麻醉医生
NVL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.ANESTHESIA_DOCTOR ),A.ANESTHESIA_DOCTOR ) ANESTHESIA_DOCTOR,
--副麻医生
NVL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.ANESTHESIA_ASSISTANT ),A.ANESTHESIA_ASSISTANT ) ANESTHESIA_ASSISTANT,
--第一洗手护士
NVL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FIRST_OPERATION_NURSE ),A.FIRST_OPERATION_NURSE ) FIRST_OPERATION_NURSE,
--第一巡回护士
NVL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FIRST_SUPPLY_NURSE ),A.FIRST_SUPPLY_NURSE ) FIRST_SUPPLY_NURSE,
--手术状态代码
A.OPER_STATUS AS OPER_STATE_CODE,
--手术状态名
DECODE(A.OPER_STATUS,0,'准备手术', 2,'准备手术',3,'诱导室',4,'诱导室',5,'入手术室',10,'麻醉开始',15,'手术开始',25,'手术结束',30,'麻醉结束',35,'出手术室',40,'准备复苏',45,'入复苏室',50,'转ICU',55,'出复苏室',60,'转入病房',80,'病案归档',-80,'手术取消') OPER_STATE,
--当前状态时间
DECODE(A.OPER_STATUS,5,TO_CHAR(A.IN_DATE_TIME,'HH24:mm'),10,TO_CHAR(A.ANES_START_TIME,'HH24:mm'),15,TO_CHAR(A.START_DATE_TIME,'HH24:mm'),25,TO_CHAR(A.END_DATE_TIME,'HH24:mm'),30,TO_CHAR(A.ANES_END_TIME,'HH24:mm'),35,TO_CHAR(A.OUT_DATE_TIME,'HH24:mm'),45,TO_CHAR(A.IN_PACU_DATE_TIME,'HH24:mm'),55,TO_CHAR(A.OUT_PACU_DATE_TIME,'HH24:mm'),60,NVL(TO_CHAR(A.OUT_PACU_DATE_TIME,'HH24:mm'),TO_CHAR(A.OUT_DATE_TIME,'HH24:mm')),'') PRO_TIME
FROM V_OPERATION_MASTER A 
LEFT OUTER JOIN V_PAT_MASTER_INDEX C ON A.PATIENT_ID = C.PATIENT_ID
LEFT OUTER JOIN V_PATS_IN_HOSPITAL D ON A.PATIENT_ID = D.PATIENT_ID AND A.VISIT_ID =  D.VISIT_ID
WHERE TO_CHAR(A.SCHEDULED_DATE_TIME,'YYYY-MM-DD') = TO_CHAR(SYSDATE,'YYYY-MM-DD') AND A.OPER_STATUS <> -80
ORDER BY A.OPERATING_ROOM,A.OPERATING_ROOM_NO,A.IN_DATE_TIME
;
