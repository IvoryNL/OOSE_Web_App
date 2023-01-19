using System.Text;

namespace Presentation.Helpers
{
    public class JwtTokenHelper
    {
        public static string GetJwtTokenFromSession(HttpContext httpContext)
        {
            return Encoding.UTF8.GetString(httpContext.Session.Get("jwtToken"));
        }
    }
}
