// Learn more about F# at http://fsharp.org

open System

open Argu

type CLIArguments =
    | Port of tcp_port:int
with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Port _ -> "specify a primary port."

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    l1.Say.hello "built paket .net core"

    let parser = ArgumentParser.Create<CLIArguments>(programName = "c1")

    try
        let args = parser.Parse argv

        let port =
            match args.GetAllResults() with
            | [Port p] -> Some (uint16 p)
            | _ -> None

        port
        |> Option.iter (l1.Say.start)

        0
    with
    | :? ArguParseException as ex ->
        printfn "%s" ex.Message
        1
    | ex ->
        printfn "Internal Error:"
        printfn "%s" ex.Message
        2
