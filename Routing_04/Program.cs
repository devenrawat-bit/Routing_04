using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Routing_04.CustomConstraints;
using System.ComponentModel;
using System.Runtime.Intrinsics.X86;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(monthsCustomConstraints));
    //the above line is used to register the custom constraint in the program.cs file so that we can use it in the map method
    //months here we will use in nowownward for the custom constrainsts, just like others now we will use the months in the constraint like we do in length, max, min, int etc we will use the months {month:months} like this 
});
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Map("map1",async (context) =>
{
       await context.Response.WriteAsync("Map 1");
});
app.Map("map2",async (context) =>
{
       await context.Response.WriteAsync("Map 2");
});

//now keep in mind the map middleware is added at the end of the pipeline so not to use the map.use after the map 

//or 👉 Map / MapGet / MapFallback endpoints pipeline ke end me execute hote hain
//Isliye unke baad app.Use(...) likhna useless hota hai.


//similarly if we can also use mappost, map get to invoke that endpoint middleware only for the get or post request like app.mapget("map3", ...), app.mappost("map4", ...)

//whichever part of the url can vary that are called the route parameteres also it can be represented with the curly braces like {id}, {name} etc, the fixed thing in the url is called the literal part of the url

//route parameter values are also called as route values 

app.Map("files/{filename}.{extension}", async (context) =>

//app.Map("files/{filename}.{extension?}", async (context) => for the optional parameter then just suffix that with the question mark then it will give the null value if there is no value supplied for the extension in the url 
//context.request.routevalues.containskey("extension"))


//👉 ContainsKey("x") use karo jab sure nahi ho key exist karti hai ya nahi
//👉 ["x"] direct use karo jab 100% sure ho key exist karegi👉 ContainsKey("x") use karo jab sure nahi ho key exist karti hai ya nahi
//👉 ["x"] direct use karo jab 100% sure ho key exist karegi

//route constraints bhi hote hain jisme hum specify kar sakte hain ki kaunsa route parameter kis type ka hoga jaise int, guid, datetime etc
//app.Map("url/{id:guid}",  () =>
//{
//like this there are more types of route constraints like int, datetime, decimal, double, float, long etc
//});
//min length like => 
//{ id:minlength(3) } => iska matlab hai ki id ka minimum length 3 hona chahiye
//{id:maxlength(5)} => iska matlab hai ki id ka maximum length 5 hona chahiye   
//{id:length(4)} => iska matlab hai ki id ka length exactly 4 hona chahiye
//{id:length(3,5)} =>only between 3 to 5 length
//{id:range(1,100)} => iska matlab hai ki id ka value 1 se 100 ke beech hona chahiye
//{id:min(10)} => iska matlab hai ki id ka value minimum 10 hona chahiye
//{id:max(100)} => iska matlab hai ki id ka value maximum 100 hona chahiye
//{name:alpha} => iska matlab hai ki name sirf alphabetic characters hone chahiye
//{code:regex(^[a-zA-Z0-9]*$)} => iska matlab hai ki code sirf alphanumeric characters hone chahiye
//multiple constraints bhi laga sakte hain jaise {id:int:min(1):max(100)}



//app.Map("files/{filename}.{extension=txt }", async (context) =>  for the default route parameter we will do = in front of the route parameter
{
    string? fileName = Convert.ToString( context.Request.RouteValues["filename"]);//important here that the route parameter returns the data in the object form so we have to convert it into the string format 
    //route parameter can never have a space 

    string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
    await context.Response.WriteAsync(fileName+"."+extension);
});
app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync("Fallback");
});



app.Run();
