module SampleAppQuartz.UnitTests

open System
open NUnit.Framework
open SampleAppQuartz.Jobs

[<SetUp>]
let Setup () =
    ()

[<Test>]
let TestJobIdGeneration () =
    let instance1 = JobTypes.HelloJob()
    let instance2 = JobTypes.HelloJob()
    Assert.That(instance1.Id.Equals(instance2.Id), Is.False)
    Console.WriteLine($"ID1: {instance1.Id}\nID2: {instance2.Id}")