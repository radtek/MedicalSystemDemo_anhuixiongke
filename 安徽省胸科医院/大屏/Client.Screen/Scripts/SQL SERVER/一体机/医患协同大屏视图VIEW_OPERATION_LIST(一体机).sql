CREATE VIEW VIEW_OPERATION_LIST AS
SELECT
       A.PATIENT_ID,                                       --病人ID (住院号)
       A.VISIT_ID,                                         --本次住院标示
       A.SCHEDULE_ID,                                      --手术申请ID
       E.NAME,                                             --名称
       E.SEX,                                              --性别
       C.INP_NO,                                           --住院号
       E.DATE_OF_BIRTH,                                    --出生日期
       E.NATION,                                           --民族
       --病人所在科室
       ISNULL((SELECT V_DEPT_DICT.DEPT_NAME   FROM V_DEPT_DICT WHERE V_DEPT_DICT.DEPT_CODE = A.DEPT_STAYED ),A.DEPT_STAYED ) DEPT_STAYED,
       A.BED_NO,                                           --床号
       A.SCHEDULED_DATE_TIME,                              --手术日期及时间
       --手术室
       A.OPERATING_ROOM,
       A.OPERATING_ROOM_NO,                                --手术间索引
       D.BED_LABEL,                                        --手术间名称
       A.SEQUENCE,                                         --台次
       A.DIAG_BEFORE_OPERATION,                            --术前诊断
       A.PATIENT_CONDITION,                                --特殊情况
       A.OPERATION_NAME ,                                  --手术名称
       A.OPERATION_SCALE,                                  --手术等级
        --手术科室
       ISNULL((SELECT V_DEPT_DICT.DEPT_NAME   FROM V_DEPT_DICT WHERE V_DEPT_DICT.DEPT_CODE = A.OPERATING_DEPT ),A.OPERATING_DEPT ) OPERATING_DEPT,
       --手术者
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.SURGEON ),A.SURGEON ) SURGEON,
       --第一手术助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FIRST_ASSISTANT ),A.FIRST_ASSISTANT ) FIRST_ASSISTANT,
       --第二手术助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.SECOND_ASSISTANT ),A.SECOND_ASSISTANT ) SECOND_ASSISTANT,
       --第三手术助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.THIRD_ASSISTANT ),A.THIRD_ASSISTANT ) THIRD_ASSISTANT,
       --第四手术助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FOURTH_ASSISTANT ),A.FOURTH_ASSISTANT ) FOURTH_ASSISTANT,
       A.ANESTHESIA_METHOD,                                --麻醉方法
       --麻醉医生
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.ANESTHESIA_DOCTOR ),A.ANESTHESIA_DOCTOR ) ANESTHESIA_DOCTOR,
       --第一麻醉助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.ANESTHESIA_ASSISTANT ),A.ANESTHESIA_ASSISTANT ) ANESTHESIA_ASSISTANT,
       --第二麻醉助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.SECOND_ANESTHESIA_ASSISTANT ),A.SECOND_ANESTHESIA_ASSISTANT ) SECOND_ANESTHESIA_DOCTOR,
       --第三麻醉助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.THIRD_ANESTHESIA_ASSISTANT ),A.THIRD_ANESTHESIA_ASSISTANT ) THIRD_ANESTHESIA_DOCTOR,
       --第四麻醉助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FOURTH_ANESTHESIA_ASSISTANT ),A.FOURTH_ANESTHESIA_ASSISTANT ) FOURTH_ANESTHESIA_ASSISTANT,
       --第一洗手护士
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FIRST_OPERATION_NURSE ),A.FIRST_OPERATION_NURSE ) FIRST_OPERATION_NURSE,
       --第二洗手护士
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.SECOND_OPERATION_NURSE ),A.SECOND_OPERATION_NURSE ) SECOND_OPERATION_NURSE,
       --第一巡回护士
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FIRST_SUPPLY_NURSE ),A.FIRST_SUPPLY_NURSE ) FIRST_SUPPLY_NURSE,
       --第二巡回护士
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.SECOND_SUPPLY_NURSE ),A.SECOND_SUPPLY_NURSE ) SECOND_SUPPLY_NURSE,
       --第三巡回护士
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.THIRD_SUPPLY_NURSE ),A.THIRD_SUPPLY_NURSE ) THIRD_SUPPLY_NURSE,
       A.NOTES_ON_OPERATION,                               --备注
       A.REQ_DATE_TIME,                                    --申请日期时间
       A.EMERGENCY_IND,                              --急诊标志
			 A.ISOLATION_IND,                                    --隔离标志
			 A.RADIATE_IND,                                      --放射标志
			 A.INFECTED_IND,                                     --感染标志
       A.OPERATION_POSITION,                               --体位
       CASE A.ANESTHESIA_METHOD WHEN '局麻' THEN '术中' ELSE '术前' END  TYPE,
       A.OPER_STATUS,
       TO_CHAR(SYSDATE,'YYYY') - TO_CHAR(E.DATE_OF_BIRTH,'YYYY') AS PAT_AGE
       FROM
       (SELECT * FROM V_OPERATION_SCHEDULE  WHERE TO_CHAR(SCHEDULED_DATE_TIME,'yyyyMMdd') = TO_CHAR(SYSDATE ,'yyyyMMdd')  AND STATE = 2) A , V_PATS_IN_HOSPITAL C,
       V_OPERATING_ROOM D,V_PAT_MASTER_INDEX E
       WHERE
       A.PATIENT_ID = C.PATIENT_ID              AND
			 A.VISIT_ID = C.VISIT_ID                  AND
       A.OPERATING_ROOM = D.DEPT_CODE AND A.OPERATING_ROOM_NO = D.ROOM_NO  AND
			 A.PATIENT_ID = E.PATIENT_ID
UNION
SELECT
       A.PATIENT_ID,                                       --病人ID (住院号)
       A.VISIT_ID,                                         --本次住院标示
       A.OPER_ID,                                          --手术申请ID
       F.NAME,                                             --名称
       F.SEX,                                              --性别
       D.INP_NO,                                           --住院号
       F.DATE_OF_BIRTH,                                    --出生日期
       F.NATION,                                           --民族
       --病人所在科室
      ISNULL((SELECT V_DEPT_DICT.DEPT_NAME   FROM V_DEPT_DICT WHERE V_DEPT_DICT.DEPT_CODE = A.DEPT_STAYED ),A.DEPT_STAYED ) DEPT_STAYED,
       --C.BED_NO,                                           --床号
       A.BED_NO,
       A.START_DATE_TIME,                                  --手术日期及时间
       --手术室
       A.OPERATING_ROOM,
       A.OPERATING_ROOM_NO,                                --手术间索引
       E.BED_LABEL,                                        --手术间名称
       A.SEQUENCE,                                         --台次
       A.DIAG_BEFORE_OPERATION,                            --术前诊断
       A.PATIENT_CONDITION,                                --特殊情况
       A.OPERATION_NAME ,                                       --手术名称
       A.OPERATION_SCALE,                                  --手术等级
       --手术科室
      ISNULL((SELECT V_DEPT_DICT.DEPT_NAME   FROM V_DEPT_DICT WHERE V_DEPT_DICT.DEPT_CODE = A.OPERATING_DEPT ),A.OPERATING_DEPT ) OPERATING_DEPT,
       --手术者
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.SURGEON ),A.SURGEON ) SURGEON,
       --第一手术助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FIRST_ASSISTANT ),A.FIRST_ASSISTANT ) FIRST_ASSISTANT,
       --第二手术助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.SECOND_ASSISTANT ),A.SECOND_ASSISTANT ) SECOND_ASSISTANT,
       --第三手术助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.THIRD_ASSISTANT ),A.THIRD_ASSISTANT ) THIRD_ASSISTANT,
       --第四手术助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FOURTH_ASSISTANT ),A.FOURTH_ASSISTANT ) FOURTH_ASSISTANT,
       A.ANESTHESIA_METHOD,                                --麻醉方法
       --麻醉医生
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.ANESTHESIA_DOCTOR ),A.ANESTHESIA_DOCTOR ) ANESTHESIA_DOCTOR,
       --第一麻醉助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.ANESTHESIA_ASSISTANT ),A.ANESTHESIA_ASSISTANT ) ANESTHESIA_ASSISTANT,
       --第二麻醉助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.SECOND_ANESTHESIA_ASSISTANT ),A.SECOND_ANESTHESIA_ASSISTANT ) SECOND_ANESTHESIA_DOCTOR,
       --第三麻醉助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.THIRD_ANESTHESIA_ASSISTANT ),A.THIRD_ANESTHESIA_ASSISTANT ) THIRD_ANESTHESIA_DOCTOR,
       --第四麻醉助手
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FOURTH_ANESTHESIA_ASSISTANT ),A.FOURTH_ANESTHESIA_ASSISTANT ) FOURTH_ANESTHESIA_ASSISTANT,
       --第一洗手护士
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FIRST_OPERATION_NURSE ),A.FIRST_OPERATION_NURSE ) FIRST_OPERATION_NURSE,
       --第二洗手护士
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.SECOND_OPERATION_NURSE ),A.SECOND_OPERATION_NURSE ) SECOND_OPERATION_NURSE,
       --第一巡回护士
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.FIRST_SUPPLY_NURSE ),A.FIRST_SUPPLY_NURSE ) FIRST_SUPPLY_NURSE,
       --第二巡回护士
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.SECOND_SUPPLY_NURSE ),A.SECOND_SUPPLY_NURSE ) SECOND_SUPPLY_NURSE,
       --第三巡回护士
       ISNULL( ( SELECT  V_HIS_USERS.USER_NAME FROM V_HIS_USERS WHERE V_HIS_USERS.USER_ID = A.THIRD_SUPPLY_NURSE ),A.THIRD_SUPPLY_NURSE ) THIRD_SUPPLY_NURSE,
       NULL,                                               --备注
       TO_DATE(NULL,'YYYY-MM-DD'),                         --申请日期时间
			 A.EMERGENCY_IND,                              --急诊标志
			 A.ISOLATION_IND,                                    --隔离标志
			 A.RADIATE_IND,                                      --放射标志
			 A.INFECTED_IND,                                     --感染标志
       A.OPERATION_POSITION,                               --体位
       CASE A.ANESTHESIA_METHOD WHEN '局麻' THEN '术中' ELSE
       DECODE( A.OPER_STATUS, 5,'术中',10,'术中',15,'术中',25,'术中',30,'术中',35,'术后',40,'复苏室',45,'复苏室') END  TYPE,
       A.OPER_STATUS,
       TO_CHAR(SYSDATE,'YYYY') - TO_CHAR(F.DATE_OF_BIRTH,'YYYY') AS PAT_AGE
       FROM
       (SELECT * FROM V_OPERATION_MASTER WHERE OPER_STATUS > 0  AND
       --2011.05.31入手术室时改变的IN_DATE_TIME，而开始手术改变的是START_DATE_TIME，所以要两个时间都判断
       (TO_CHAR(START_DATE_TIME , 'yyyyMMdd') = TO_CHAR( SYSDATE,'yyyyMMdd')  OR
       TO_CHAR(IN_DATE_TIME , 'yyyyMMdd') = TO_CHAR( SYSDATE ,'yyyyMMdd')) AND
       --TRUNC((SYSDATE-ISNULL(OUT_DATE_TIME,SYSDATE))*1440 ) <  5   AND  --这个5代表术后5分钟不显示
       OPER_STATUS < 50 ) A , V_ANESTHESIA_PLAN C , V_PATS_IN_HOSPITAL D,V_OPERATING_ROOM E,V_PAT_MASTER_INDEX F
       WHERE
       A.PATIENT_ID = D.PATIENT_ID                            AND
			 A.VISIT_ID =D.VISIT_ID                                AND
       A.PATIENT_ID = C.PATIENT_ID(+)                            AND
       A.VISIT_ID = C.VISIT_ID(+)                                AND
       A.OPER_ID = C.OPER_ID(+)                                  AND
       A.OPERATING_ROOM = E.DEPT_CODE                            AND 
			 A.OPERATING_ROOM_NO = E.ROOM_NO                           AND
			 A.PATIENT_ID = F.PATIENT_ID
;
