
using System.Text.RegularExpressions;

namespace Routing_04.CustomConstraints
{
    //to make your own custom constraint, you need to implement the IRouteConstraint interface
    public class monthsCustomConstraints : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, 
            IRouter? route, //the route which is being processed
            string routeKey, //the actual route parameter name
            RouteValueDictionary values, //the route values collection
//            values = {
//            "month" : "apr"
//} automatically created by the asp.net 
        RouteDirection routeDirection) //whether the route is being incoming request or outgoing response
        {
            //when ever you want to use the complex constraints or a resuable constraints then you can create your own custom constraints by implementing the IRouteConstraint interface

            //first we will check whether the value exists or not 
            if (!values.ContainsKey(routeKey))
            {
                return false;
            }   
            //the above means no value matches
            Regex regex= new Regex($"^(apr|may|jun|jul|aug)$"); 
            string? monthValue = Convert.ToString(values[routeKey]);   //this gaves the apr like data not the parameter month  
            if(regex.IsMatch(monthValue!))//here the ismatch is the inbuilt method 
            {
                return true; //means value matches
            }
            return false; //means no value matches
        }
    }//now remeber before using this custom constraint we have to register it in the program.cs file
}
