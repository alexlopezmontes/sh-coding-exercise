General instructions:

- Set the Worker project as the Startup project.
- Ensure you have obtained the necessary NuGet packages
- Note that I have simulated responses from the APIs for unit tests but haven't done so for when the worker runs.

Comments for OrderApiGateway

- I wasn't sure if we'd hipotetically have control over the API we consume. If so, I'd recommend adding an enpoint that allows us filter by status, such as GET - api/orders?status=Delivered.
- 
Comments for Orders Worker

- The idea is to add a Background job processing framework such as hangfire.  I felt that adding too much robustness might delay the application submission, so I've kept it relatively simple for now.
- 
Comments For Utility

- I'm naming the folder as Constants for readability although I'm using static readonly strings to not having the compiler create a copy of the constant value on each assembly.

For Settings

- My preference is to define the environment-specific setting to be copied by CI/CD pipelines, instead of creating  appsettings.{environment}.json files.
