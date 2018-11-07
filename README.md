# ADAL-.NET-Console-Application
This is an ADAL .net Console application that will get 5 users from the Microsoft Graph in the tenant that you define in the variables for the program.cs file.

These variables are listed here : https://gist.github.com/frankhu1234/ba87de35d6a9393f550bc3f578955629

They are at the top of the Program.CS file in the repo. https://github.com/frankhu1234/ADAL-.NET-Console-Application/blob/master/MyTestADALConsoleApp/Program.cs

The creation of this Application is explained in the AAD Dev Support blogs : https://blogs.msdn.microsoft.com/aaddevsup/2018/11/07/how-to-use-the-adal-net-library-to-call-the-microsoft-graph-api-in-a-console-application-using-authorization-code-flow/

This will get an access token using ADAL V4.3 and then get 5 users from the Microsoft Graph API 

In addition to that, if you're interested in the Microsoft Graph SDK, feel free to follow this blog post to learn how to do that to extend off of this github repo:  
https://blogs.msdn.microsoft.com/aaddevsup/2018/11/07/how-to-use-the-net-microsoft-graph-sdk-to-get-users-and-get-next-page-of-results/


