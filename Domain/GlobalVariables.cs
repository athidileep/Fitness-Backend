using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GlobalVariables
    {
        #region User
        #region Info
        #endregion

        #region Error

        public const int ValidUserDetails = 1000;
        public const string ValidUserDetailsDescription = "Received Valid User Details";

        public const int emptyUserName = 1001;
        public const string emptyUserNameDescription = "Received empty user name";

        public const int emptyUserType = 1002;
        public const string emptyUserTypeDescription = "Received empty user type";

        public const int emptyUserPassword = 1003;
        public const string emptyUserPasswordDescription = "Received empty passowrd";

        public const int receivedInvalidUser = 1004;
        public const string receivedInvalidUserDescription = "Received invalid username or password";

        public const int emptyUserJSON = 1005;
        public const string emptyUserJSONDescription = "Received Empty JSON for User Vallidation";

        public const int emptySASSId = 1006;
        public const string emptySASSIdDescription = "Received empty SASS ID";

        public const int errorUserValidation = 1007;
        public const string errorUserValidationDescription = "Error while validating the User authentication";

        public const int emptyUserIdForProfile = 1008;
        public const string emptyUserIdForProfileDescription = "Received empty user Id for user profile";

        public const int emptyFirstname = 1009;
        public const string emptyFirstnameDescription = "Received empty first name";

        public const int emptyLastname = 1010;
        public const string emptyLastnameDescription = "Received empty last name";

        public const int emptyDateOfBirth = 1011;
        public const string emptyDateOfBirthDescription = "Received empty date of birth";

        public const int emptyGender = 1012;
        public const string emptyGenderDescription = "Received empty gender";

        public const int emptyMaritalStatus = 1013;
        public const string emptyMaritalStatusDescription = "Received empty marital status";

        public const int emptyAnniversaryDate = 1014;
        public const string emptyAnniversaryDateDescription = "Received empty anniversary date";

        public const int emptyState = 1015;
        public const string emptyStateDescription = "Received empty state";

        public const int emptyCountry = 1016;
        public const string emptyCountryDescription = "Received empty country";

        public const int emptyHeight = 1017;
        public const string emptyHeightDescription = "Received empty height";

        public const int emptyWeight = 1018;
        public const string emptyWeightDescription = "Received empty weight";

        public const int emptyMemberFrom = 1019;
        public const string emptyMemberFromDescription = "Received empty member from";

        public const int emptyMemberTo = 1020;
        public const string emptyMemberToDescription = "Received empty member to";

        public const int emptyEmail = 1021;
        public const string emptyEmailDescription = "Received empty email";

        public const int emptyContactNumber = 1022;
        public const string emptyContactNumberDescription = "Received empty contact number";

        public const int emptyEmergencyContactNumber = 1023;
        public const string emptyEmergencyContactNumberDescription = "Received empty emergency contact number";

        public const int emptyEmergencyContactPerson = 1024;
        public const string emptyEmergencyContactPersonDescription = "Received empty emergency contact person";

        public const int emptyAdress = 1025;
        public const string emptyAdressDescription = "Received empty address";

        public const int emptyFalseMedicalCondition = 1026;
        public const string emptyFalseMedicalConditionDescription = "Received empty medical condition details even though the medical condition is false";

        public const int emptyMedicalDetails = 1027;
        public const string emptyMedicalDetailsDescription = "Received empty medical condition details";

        public const int emptyFalseMedication = 1028;
        public const string emptyFalseMedicationDescription = "Received empty medication even though the medication is false";

        public const int emptyMedicationDetails = 1029;
        public const string emptyMedicationDetailsDescription = "Received empty medication details";

        public const int UserProfileUpdateSuccess = 1030;
        public const string UserProfileUpdateSuccessDescription = "UserProfile Updated Successfully";

        public const int UserProfileUpdateIDError = 1031;
        public const string UserProfileUpdateIDErrorDescription = "Received invalid primary key for update UserProfile";

        public const int UserProfileUpdateError = 1032;
        public const string UserProfileUpdateErrorDescription = "Error while updating data into UserProfile table";

        public const int UserProfileUpdateNoData = 1033;
        public const string UserProfileUpdateNoDataDescription = "No record was found matching the specified primary key";

        public const int UserProfilesavechangesError = 1034;
        public const string UserProfilesavechangesErrorDescription = "Error while  update the data into UserProfile table";

        public const int MedicalInfoUpdateNoData = 1035;
        public const string MedicalInfoUpdateNoDataDescription = "No record was found matching the specified primary key";

        #endregion
        #endregion

        #region Goal Setting

        #region GoalSetting Tracking 

        public const int GoalSettingInsertSuccess = 2001;
        public const string GoalSettingInsertSuccessDescription = "User GoalSetting Inserted Successfully";

        public const int GoalSettingUpdateSuccess = 2002;
        public const string GoalSettingUpdateSuccessDescription = "User GoalSetting Updated Successfully";

        public const int emptyGoalSettingGoalType = 2003;
        public const string emptyGoalSettingGoalTypeDescription = "Received empty/invalid GoalType";

        public const int emptyGoalSettingTargetWeight = 2004;
        public const string emptyGoalSettingTargetWeightDescription = "Received empty/invalid TargetWeight";

        public const int emptyGoalSettingTargetMuscleMass = 2005;
        public const string emptyGoalSettingTargetMuscleMassDescription = "Received empty/invalid Target Muscle Mass";

        public const int emptyGoalSettingTargetDuration = 2006;
        public const string emptyGoalSettingTargetDurationDescription = "Received empty/invalid Target Duration";

        public const int emptyGoalSettingWorkoutFrequency = 2007;
        public const string emptyGoalSettingWorkoutFrequencyDescription = "Received empty/invalid Workout Frequency";

        public const int emptyGoalSettingWorkoutDuration = 2008;
        public const string emptyGoalSettingWorkoutDurationDescription = "Received empty/invalid Workout Duration";

        public const int GoalSettingUpdateNoData = 2009;
        public const string GoalSettingUpdateNoDataDescription = "No record was found matching the specified primary key";

        public const int GoalSettingsavechangesError = 2010;
        public const string GoalSettingsavechangesErrorDescription = "Error while  update the data into GoalSetting Tracking table";

        public const int GoalSettingUpdateIDError = 2011;
        public const string GoalSettingUpdateIDErrorDescription = "Received invalid primary key for update GoalSetting Tracking";

        public const int GoalSettingUpdateError = 2012;
        public const string GoalSettingUpdateErrorDescription = "Error while updating data into GoalSetting Tracking table";

        public const int GoalSettingAlreadyExist = 2013;
        public const string GoalSettingAlreadyExistDescription = "GoalSetting Already Exist for the User";

        public const int ErrorOnInsertingGoalSetting = 2014;
        public const string ErrorOnInsertingGoalSettingDescription = "Error while inserting the GoalSetting details";

        public const int GoalSettingDosntExist = 2015;
        public const string GoalSettingDosntExistDescription = "GoalSetting dosn't exist for the User";

        public const int DailyGoalSettingAlreadyExist = 2016;
        public const string DailyGoalSettingAlreadyExistDescription = "GoalSetting Already Exist for the User";

        public const int emptyJSON = 2017;
        public const string emptyJSONDescription = "Received Empty JSON for Insert GoalSetting Tracking";

        public const int GoalSettingInsertError = 2018;
        public const string GoalSettingInsertErrorDescription = "Error while inserting data into GoalSetting table";

        public const int GoalAlreadyExist = 2019;
        public const string GoalAlreadyExistDescription = "Goal Already Exist for the User";

        public const int ErrorOnInsertingGoal = 2020;
        public const string ErrorOnInsertingGoalDescription = "Error while inserting the Goal details";

        public const int GoalDosntExist = 2021;
        public const string GoalDosntExistDescription = "Goal dosn't exist for the User";

        public const int emptyGoalSettingTargetBodyFat = 2022;
        public const string emptyGoalSettingTargetBodyFatDescription = "Received empty/invalid TargetBodyFat";



        #endregion


        #endregion

        #region Activity Tracking 

        public const int ActivityInsertSuccess = 1101;
        public const string ActivityInsertSuccessDescription = "User Activity Inserted Successfully";

        public const int ActivityeUpdateSuccess = 1102;
        public const string ActivityUpdateSuccessDescription = "User Activity Updated Successfully";

        public const int InvalidActivityId = 1000;
        public const string InvalidActivityIdDescription = "Received empty/invalid Id";

        public const int emptyActivityDate = 1103;
        public const string emptyActivityDateDescription = "Received empty/invalid Date";

        public const int emptyActivityWeight = 1104;
        public const string emptyActivityWeightDescription = "Received empty/invalid Weight";

        public const int emptyActivityType = 1105;
        public const string emptyActivityTypeDescription = "Received empty/invalid Type";

        public const int emptyActivityName = 1105;
        public const string emptyActivityNameDescription = "Received empty/invalid Activity Name";

        public const int emptyActivityIntensity = 1106;
        public const string emptyActivityIntensityDescription = "Received empty/invalid Intensity Level";

        public const int ActivityUpdateNoData = 1107;
        public const string ActivityUpdateNoDataDescription = "No record was found matching the specified primary key";

        public const int ActivitysavechangesError = 1108;
        public const string ActivitysavechangesErrorDescription = "Error while  update the data into Activity Tracking table";

        public const int ActivityUpdateIDError = 1109;
        public const string ActivityUpdateIDErrorDescription = "Received invalid primary key for update Activity Tracking";

        public const int ActivityUpdateError = 1110;
        public const string ActivityUpdateErrorDescription = "Error while updating data into Activity Tracking table";

        public const int ActivityAlreadyExist = 1111;
        public const string ActivityAlreadyExistDescription = "Activity Already Exist for the User";

        public const int ErrorOnInsertingActivity = 1112;
        public const string ErrorOnInsertingActivityDescription = "Error while inserting the Activity details";

        public const int ActivityDosntExist = 1113;
        public const string ActivityDosntExistDescription = "Activity dosn't exist for the User";

        public const int DailyActivityAlreadyExist = 1114;
        public const string DailyActivityAlreadyExistDescription = "Activity Already Exist for the User";

        public const int emptysJSON = 1115;
        public const string emptysJSONDescription = "Received Empty JSON for Insert Activity Tracking";

        public const int ActivityInsertError = 1116;
        public const string ActivityInsertErrorDescription = "Error while inserting data into Activity table";

        public const int emptyDuration = 1117;
        public const string emptyDurationDescription = "Received empty/invalid Duration";

        public const int emptyReps = 1118;
        public const string emptyRepsDescription = "Received empty/invalid Reps";

        public const int emptySets = 1119;
        public const string emptySetsDescription = "Received empty/invalid Sets";

        public const int emptyLiftWeight = 1120;
        public const string emptyLiftWeightDescription = "Received empty/invalid Weight(Lifting)";

        public const int emptyCaloriesBurned = 1121;
        public const string emptyCaloriesBurnedDescription = "Received empty/invalid Calories Burned";

        public const int emptyNotes = 1122;
        public const string emptyNotesDescription = "Received empty/invalid Notes";

        public const int ActivityValidateError = 1123;
        public const string ActivityValidateErrorDescription = "Error while validating received Activity data";

        public const int EmptyActivityTabledata = 1124;
        public const string EmptyActivityTabledataDescription = "Received Empty/Invalid Activity Bulk Upload Data";

        public const int emptyActivityBodyFat = 1125;
        public const string emptyActivityBodyFatDescription = "Received empty/invalid Body Fat";

        public const int emptyActivitMuscleMass = 1126;
        public const string emptyActivitMuscleMassDescription = "Received empty/invalid Muscle Mass";

        #endregion
    }
}
