using Base.Core.Schemas; 

namespace Base.Application.UseCase.User.Crud
{
    public class CrudUserPresenter
    {  
        public static List<UserSchema> PresentList(List<UserSchema> items)
        {
            var result = new List<UserSchema>();
            foreach (var item in items)
            {
                var user = PresentItem(item);
                result.Add(user);
            }
            return result;
        }

        public static UserSchema PresentItem(UserSchema item)
        {
            UserSchema user = new UserSchema();
            user.Id = item.Id;
            return user;
        }
    }
}
