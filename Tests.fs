module Tests

open System
open Xunit
open System.Text



let text = new StringBuilder()

let printToSb (s:string) =
    text.AppendLine(s) |> ignore


let showImmutableVersion () =
    let asm = typeof<System.Collections.Immutable.ImmutableArray>.Assembly
    
    printToSb $"typeof<immarray> location {asm.Location}"
    printToSb $"typeof<immarray> ass version: {asm.GetName().Version.ToString()}"

let showAllLoadedImmutableVersions() =
    let loadedAssemblies = System.AppDomain.CurrentDomain.GetAssemblies()
    loadedAssemblies
    |> Array.filter (fun asm -> asm.FullName.Contains("Collections.Immutable"))
    |> Array.iter (fun asm -> 
        printToSb $"Loaded: {asm.FullName} from { asm.Location}" )

let testImmutableArray() = 
    let x = System.Collections.Immutable.ImmutableArray.Create<int>(System.ReadOnlySpan.op_Implicit([|1;2;3|]))
    printToSb $"Created ImmutableArray with length: {x.Length} ;; {x}"

[<Fact>]
let ``My test`` () =
    showImmutableVersion()
    showAllLoadedImmutableVersions()
    testImmutableArray()  //uncomment to see it crash with a missing method exn
    Assert.True(false,text.ToString())
    

[<EntryPoint>]
let main _args = 
    ``My test``()
    0