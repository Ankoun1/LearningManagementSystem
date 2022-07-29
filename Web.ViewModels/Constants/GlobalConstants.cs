namespace Web.ViewModels.Constants
{
    public static class GlobalConstants
    {
        public class UserConstants
        {
            public const string FullNameRegex = @" *([A-za-z]{2,}) +([A-za-z]{2,}) *";
            public const string FullNameError = "FullName is invalid. Must contain at least two separate names with letters only.";


            public const byte MaxLengthName = 14;
            public const byte MinLengthName = 6;

            public const byte PasswordMaxLength = 10;
            public const byte PasswordMinLength = 6;
           

            public const string RoleAdmin = "Admin";
            public const string RoleOperator = "Operator";
            public const string RoleStudent = "Student";
            public const string RoleTeacher = "Teacher";           

            public const string AdminName1 = "Gosho Goshov";
            public const string AdminName2 = "Pesho Peshov";
            public const string AdminEmail1 = "Gosho@gmail.com";
            public const string AdminEmail2 = "Pesho@gmail.com";
            

            public const string AdminPassword1 = "123456";
            public const string AdminPassword2 = "1234567";
           
        }

        public class CourseConstants
        {
            public const byte MaxLengthTitel = 50;
            public const byte MinLengthTitel = 1;

            public const byte MaxLengthDesc = 200;
            public const byte MinLengthDesc = 50;

            public const byte MaxDifficulty = 5;
            public const byte MinDifficulty = 1;
        } 
        public class MaterialConstants
        {
            public const byte MaterialMaxLength = 10;
            public const byte MaterialMinLength = 5;
        }
        
        public class ModulConstants
        {
            public const byte DescriptionMaxLength = 200;
            public const byte DescriptionMinLength = 50;

            public const byte TitelMaxLength = 20;
            public const byte TitelMinLength = 5;
        }
        
        public class TestConstants
        {
            public const byte TypeMaxLength = 10;
            public const byte TypeMinLength = 5;

            public const int LinkMaxLength = 1000;
            public const byte LinkMinLength = 100;
        }
        
        public class QuestionConstants
        {            
            public const byte DescriptionMinLength = 100;            
        }
    }
}
