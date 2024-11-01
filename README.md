# Revit Add-in ParameterScanner Installation Guide

## Overview

This Revit Add-in allows users to search for parameters within Revit elements based on specified names and values. The user interface is built using WPF and provides options to isolate elements in the view or select them directly.

## Prerequisites

- Autodesk Revit 2020
- .NET Framework 4.8 or later

## Installation Steps

### 1. Download the Add-in

Download the add-in (DLL file) and the necessary `.addin` file for Revit from the provided link.

### 2. Locate the Revit Add-ins Folder

Navigate to the Revit Add-ins folder on your system. The path is usually:

- **Windows 10/11:**  
  `C:\Users\<YourUsername>\AppData\Roaming\Autodesk\Revit\Addins\2020`

### 3. Copy the Files

1. **Copy the Add-in DLL File:**
   - Place the ParameterScanner.dLL file of your add-in in the Add-ins folder located above.

2. **Copy the .addin File:**
   - Place the `Parameter.addin` XML file in the same Add-ins folder. The `Parameter.addin` file allows Revit to recognize and load your add-in.

### 4. Check the Parameter.addin File

Ensure that the `Parameter.addin` file contains the correct path to your DLL. Here’s an example of what the contents might look like:

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<RevitAddIns>
    <AddIn Type="Application">
        <Name>Parameter Add-in</Name>
	<FullClassName>ParameterScanner.App</FullClassName>
	<Text>Parameter</Text>
        <Description>Parameters toolbar.</Description>
        <Assembly>ParameterScanner.dll</Assembly>
        <AddInId>0E30C873-E2E2-40AF-99D7-99ECF44CFFE2</AddInId>
        <VendorId>2022630695</VendorId>
        <VendorDescription>IPN</VendorDescription>
        <LanguageType>CSHARP</LanguageType>
    </AddIn>
</RevitAddIns>
