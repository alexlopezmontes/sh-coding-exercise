General instructions:

- Set the Worker project as the Startup project.
- Get Nuget packages.


Comments for OrderApiGateway

  1.  I wasn't sure if we'd hipotetically have control over that one. If so, I'd recommend adding an enpoint that allows us filter by status, such as GET - api/orders?status=Delivered

Comments for Orders Worker
  1.  The idea is to add a Background job processing framework such as hangfire.  I felt like I was adding too much robustness and don't want to delay submitting the application even more.

Comments For Utility
   1. I'm naming the folder as Constants for readability although I'm using static readonly strings to not having the compiler create a copy of the constant value on each assembly.


For Settings.
   1. My preference is to define the environment-specific setting to be copied by CI/CD pipelines, instead of creating  appsettings.{environment}.json files.
