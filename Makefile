build_solution:
	dotnet build ./SampleAppQuartz

run_jobs:
	dotnet run --project ./SampleAppQuartz/SampleAppQuartz

unit_test:
	dotnet test ./SampleAppQuartz/SampleAppQuartz.UnitTests