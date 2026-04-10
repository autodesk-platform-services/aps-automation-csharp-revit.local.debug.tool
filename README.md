# APS Revit Automation - Local debug tool

![Platforms](https://img.shields.io/badge/Plugins-Windows-lightgray.svg)
![.NET](https://img.shields.io/badge/.NET%20Framework-4.8-blue.svg)
![.NET](https://img.shields.io/badge/.NET%20-8.0-blue.svg)
![.NET](https://img.shields.io/badge/.NET-10.0-blue.svg)

[![Revit](https://img.shields.io/badge/Revit-2023|2024|2025|2026|2027-lightblue.svg)](http://aps.autodesk.com/)

# Description

AutomationServiceHandler is a Revit addin that allows users to run and debug their Revit Automation application locally using desktop Revit. 

# Demonstration

See [step-by-step video](https://www.youtube.com/watch?v=i0LJ9JOpKMQ)

# Setup

## Prerequisites

1. **APS Account**: Learn how to create a APS Account, activate your subscription, and create an app at [this tutorial](http://aps.autodesk.com/tutorials/#/account/). 
2. **Visual Studio**: [2022 or newer](https://visualstudio.microsoft.com/)
3. **.NET Framework** or **.NET Core** basic knowledge with C#
4. **Revit**: required for compiling plugin changes

## Compile and Load on Revit

1. Build the solution `AutomationServiceHandler`, compiling AutomationServiceHandler for Revit 2023 (`AutomationServiceHandler2023`), Revit 2024 (`AutomationServiceHandler2024`), Revit 2025 (`AutomationServiceHandler2025`), Revit 2026 (`AutomationServiceHandler2026`), and Revit 2027 (`AutomationServiceHandler2027`)
    > Revit Automation API currently supports Revit 2023, 2024, 2025, 2026, and 2027.

    > `AutomationServiceHandler2027` targets **.NET 10** (`net10.0-windows`) to match the Revit 2027 API. Install the [.NET 10 SDK](https://dotnet.microsoft.com/download) (or a Visual Studio version that includes it) before building that project.

2. Copy/paste the `AutomationServiceHandler.addin` into the "Addins" folder `C:\ProgramData\Autodesk\Revit\Addins\XXXX\`, where `XXXX` is the Revit version (e.g. 2023, 2024, 2025, 2026, 2027) you intend to run.

    > **Note.** Currently, we remained old brand name in the name of assembly and *.addin file during the transition period. Will change them in the future.

    > The assembly name is still `DesignAutomationHandler.dll` and hasn't migrated to `AutomationServiceHandler.dll`.

    > The addin filename is still `DesignAutomationHandler.addin` and hasn't migrated to `AutomationServiceHandler.addin`.

3. Copy/paste the `.addin` file of your Revit Automation plugin into the same folder `C:\ProgramData\Autodesk\Revit\Addins\XXXX\`. 

    > AutomationServiceHandler doesn't support local testing/debugging of more than one addin at a time.

    > Revit needs the `DesignAutomationBridge.dll` to be in the same folder as your addin's dlls.

## Usage

Starting Revit:

- If your addin requires an input Revit model at startup, then open the file in Revit before running the addin or specify the file using a startup argument.

    > If you have json parameter defined to run the `WorkItem` in Automation Service, you can save the json payload as a json file in the same folder as your input Revit file.

    > For example: if `CountItParams` is the name of the parameter defined in your `Activity` of the Automation Service, then you should create a file `CountItParams.json` with contents `{"walls": false,"floors": true,"doors": true,"windows": true}` and save it to the folder which contains your input Revit model.

- In the Revit ribbon, navigate to the `Add-Ins` tab, then click `External Tools`, and click the `AutomationServiceHandler` command. Your addin will by executed! A dialog will pop-up to report the execution result. If the execution was successful, the dialog will indicate where you can find the output(s). They go to the folder which contained your input files. 

- If your plugin doesn't require any input files, start Revit and run the `AutomationServiceHandler` command without any additional setup. If you have json parameter defined to run the `WorkItem` in Automation Service, a dialog will instruct you where to put your json file.  

    > You can rerun the same addin without restarting Revit.

    > To run different addin, repeat the setup steps above.

    > Currently, the AutomationServiceHandler doesn't support multiple input files.  (This feature is supported by Automation Service on APS.)

# Further Reading

Documentation:

- [APS Automation API v3](https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/)

Desktop APIs:

- [My First Revit Plugin](https://knowledge.autodesk.com/support/revit-products/learn-explore/caas/simplecontent/content/my-first-revit-plug-overview.html)

Blog articles:

- [Learn APS tutorial for Revit](https://aps.autodesk.com/blog/introducing-design-automation-tutorial-autocad-inventor-revit-engines)
- [APS Revit Automation - Debug Revit plugin locally](https://aps.autodesk.com/blog/design-automation-debug-revit-plugin-locally)

## License

Please see the [LICENSE](LICENSE) file for full details.

## Written by

Lijuan Zhu and Ashwin Shivashankar

## Maintained by

Developer Advocacy and Support<br/>
Autodesk<br/>
http://aps.autodesk.com