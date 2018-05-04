namespace l1

open Suave

module Say =
    let hello name =
        printfn "Hello %s" name

    let start port =
        startWebServer
            { defaultConfig with
                bindings = [ HttpBinding.create HTTP System.Net.IPAddress.Any port ] }
            (Successful.OK "Hello World!")
