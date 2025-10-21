# ReceiptProcessor
# Visual Studio Setup Guide
## Receipt Processor ASP.NET Core Application

This guide will walk you through setting up the Receipt Processor project in Visual Studio 2022.

---

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [Method 1: Create New Project in Visual Studio](#method-1-create-new-project-in-visual-studio)
3. [Method 2: Use .NET CLI Then Open in Visual Studio](#method-2-use-net-cli-then-open-in-visual-studio)
4. [Method 3: Import Existing Code](#method-3-import-existing-code)
5. [Adding NuGet Packages](#adding-nuget-packages)
6. [Project Structure Setup](#project-structure-setup)
7. [Configuring the Project](#configuring-the-project)
8. [Building and Running](#building-and-running)

---

## Prerequisites

### Required Software
- **Visual Studio 2022** (Community, Professional, or Enterprise)
  - Download: [https://visualstudio.microsoft.com/downloads/](https://visualstudio.microsoft.com/downloads/)
- **Workload Required**: ASP.NET and web development
- **.NET 8.0 SDK** (usually included with Visual Studio 2022)

### Verify Installation
1. Open Visual Studio 2022
2. Go to **Help** → **About Microsoft Visual Studio**
3. Verify .NET 8.0 SDK is listed

---

## Method 1: Create New Project in Visual Studio

### Step 1: Create New Project

1. **Launch Visual Studio 2022**
2. Click **"Create a new project"** on the start window

   ![Create New Project](https://docs.microsoft.com/en-us/visualstudio/get-started/media/vs-2022/create-new-project.png)

3. In the search box, type: **"ASP.NET Core Web App"**
4. Select **"ASP.NET Core Web App (Model-View-Controller)"** with C#
5. Click **Next**

### Step 2: Configure Project

Fill in the project details:
- **Project name**: `ReceiptProcessor`
- **Location**: Choose your preferred directory (e.g., `C:\Projects\`)
- **Solution name**: `ReceiptProcessor`
- ☐ **Place solution and project in the same directory** (optional - leave unchecked for cleaner structure)

Click **Next**

### Step 3: Additional Information

Configure framework settings:
- **Framework**: `.NET 8.0 (Long Term Support)`
- **Authentication type**: `None`
- ☑ **Configure for HTTPS** (recommended)
- ☐ **Enable Docker** (leave unchecked)
- ☐ **Do not use top-level statements** (leave unchecked)

Click **Create**

### Step 4: Wait for Project Creation

Visual Studio will:
- Create the project structure
- Generate necessary files
- Restore NuGet packages
- Open the solution

**Your solution is now ready!** Proceed to [Adding NuGet Packages](#adding-nuget-packages)

---

## Method 2: Use .NET CLI Then Open in Visual Studio

### Step 1: Create Project via Command Line

Open **Command Prompt** or **PowerShell**:

```powershell
# Create solution directory
mkdir C:\Projects\ReceiptProcessorSolution
cd C:\Projects\ReceiptProcessorSolution

# Create solution file
dotnet new sln -n ReceiptProcessor

# Create MVC project
dotnet new mvc -n ReceiptProcessor

# Add project to solution
dotnet sln add ReceiptProcessor\ReceiptProcessor.csproj

# Verify
dotnet sln list
```

**Expected Output:**
```
Project(s)
----------
ReceiptProcessor\ReceiptProcessor.csproj
```

### Step 2: Open in Visual Studio

1. Navigate to `C:\Projects\ReceiptProcessorSolution\`
2. Double-click **`ReceiptProcessor.sln`**
   
   **OR**
   
3. Open Visual Studio 2022
4. Click **"Open a project or solution"**
5. Browse to `C:\Projects\ReceiptProcessorSolution\ReceiptProcessor.sln`
6. Click **Open**

---

## Method 3: Import Existing Code

If you already have the project code files:

### Step 1: Organize Files

Create this directory structure:
```
C:\Projects\ReceiptProcessorSolution\
└── ReceiptProcessor\
    ├── Controllers\
    ├── Models\
    ├── Services\
    ├── Views\
    │   ├── Home\
    │   └── Shared\
    ├── wwwroot\
    │   └── css\
    ├── Program.cs
    ├── appsettings.json
    ├── appsettings.Development.json
    └── ReceiptProcessor.csproj
```

### Step 2: Create Solution File

Open **Command Prompt** in `C:\Projects\ReceiptProcessorSolution\`:

```powershell
# Create solution file
dotnet new sln -n ReceiptProcessor

# Add existing project
dotnet sln add ReceiptProcessor\ReceiptProcessor.csproj

# Restore packages
dotnet restore
```

### Step 3: Open in Visual Studio

Double-click `ReceiptProcessor.sln` or open via Visual Studio

---

## Adding NuGet Packages

### Method A: Using NuGet Package Manager (GUI)

1. In **Solution Explorer**, right-click on **`ReceiptProcessor`** project
2. Select **"Manage NuGet Packages..."**

   ![Manage NuGet Packages](https://docs.microsoft.com/en-us/nuget/consume-packages/media/manage-nuget-packages-solution.png)

3. Click the **Browse** tab
4. Search and install each package:

| Package Name | Version | Description |
|---|---|---|
| `AWSSDK.Core` | Latest | AWS SDK Core |
| `AWSSDK.S3` | Latest | Amazon S3 Service |
| `AWSSDK.DynamoDBv2` | Latest | Amazon DynamoDB |
| `AWSSDK.Rekognition` | Latest | Amazon Rekognition |
| `AWSSDK.Extensions.NETCore.Setup` | Latest | AWS .NET Core Extensions |

**For each package:**
- Type package name in search box
- Click on the package
- Click **Install**
- Accept license agreements
- Wait for installation to complete

### Method B: Using Package Manager Console

1. Go to **Tools** → **NuGet Package Manager** → **Package Manager Console**
2. Run these commands:

```powershell
Install-Package AWSSDK.Core
Install-Package AWSSDK.S3
Install-Package AWSSDK.DynamoDBv2
Install-Package AWSSDK.Rekognition
Install-Package AWSSDK.Extensions.NETCore.Setup
```

### Method C: Edit .csproj Directly

1. In **Solution Explorer**, double-click **`ReceiptProcessor.csproj`**
2. Add this inside `<Project>` tag:

```xml
<ItemGroup>
  <PackageReference Include="AWSSDK.Core" Version="3.7.400.23" />
  <PackageReference Include="AWSSDK.DynamoDBv2" Version="3.7.400.23" />
  <PackageReference Include="AWSSDK.Extensions.NETCore.Setup" Version="3.7.301" />
  <PackageReference Include="AWSSDK.Rekognition" Version="3.7.400.23" />
  <PackageReference Include="AWSSDK.S3" Version="3.7.400.23" />
</ItemGroup>
```

3. Save the file
4. Right-click solution → **Restore NuGet Packages**

---

## Project Structure Setup

### Step 1: Create Folders

In **Solution Explorer**, right-click on **`ReceiptProcessor`** project:

1. **Add** → **New Folder** → Name: `Models`
2. **Add** → **New Folder** → Name: `Services`
3. Verify **Controllers** folder exists (should be there by default)
4. Verify **Views** folder exists

### Step 2: Add Class Files

#### Create Models/Receipt.cs

1. Right-click **Models** folder → **Add** → **Class...**
2. Name: `Receipt.cs`
3. Replace content with the code from the artifacts

#### Create Services/IReceiptService.cs

1. Right-click **Services** folder → **Add** → **Class...**
2. Name: `IReceiptService.cs`
3. Replace content with interface code

#### Create Services/ReceiptService.cs

1. Right-click **Services** folder → **Add** → **Class...**
2. Name: `ReceiptService.cs`
3. Replace content with service implementation

### Step 3: Update Existing Files

#### Update Program.cs

1. In **Solution Explorer**, double-click **`Program.cs`**
2. Replace entire content with the Program.cs code from artifacts

#### Update Controllers/HomeController.cs

1. Expand **Controllers** folder
2. Double-click **`HomeController.cs`**
3. Replace entire content with the HomeController code

### Step 4: Create Views

#### Create Views/Home/Index.cshtml

1. Right-click **Views/Home** folder → **Add** → **View...**
2. Select **Razor View - Empty**
3. Name: `Index.cshtml`
4. Replace content with Index.cshtml code from artifacts

#### Create Views/Home/Details.cshtml

1. Right-click **Views/Home** folder → **Add** → **View...**
2. Select **Razor View - Empty**
3. Name: `Details.cshtml`
4. Replace content with Details.cshtml code

#### Update Views/Shared/_Layout.cshtml

1. Expand **Views/Shared** folder
2. Double-click **`_Layout.cshtml`**
3. Replace content with _Layout.cshtml code from artifacts

### Step 5: Update Configuration Files

#### Update appsettings.json

1. In **Solution Explorer**, double-click **`appsettings.json`**
2. Replace content with:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "AWS": {
    "Profile": "default",
    "Region": "us-east-1",
    "S3BucketName": "receipt-processor-receipts-YOUR_ACCOUNT_ID",
    "DynamoDBTableName": "receipt-processor-receipts"
  }
}
```

3. **IMPORTANT**: Replace `YOUR_ACCOUNT_ID` with your actual AWS account ID from CloudFormation outputs

#### Update appsettings.Development.json

1. Double-click **`appsettings.Development.json`**
2. Add the same AWS configuration section

---

## Configuring the Project

### Step 1: Set Startup Project

1. In **Solution Explorer**, right-click **`ReceiptProcessor`** project
2. Select **"Set as Startup Project"**
3. Project name will appear in bold

### Step 2: Configure Launch Settings

1. In **Solution Explorer**, expand **Properties** folder
2. Double-click **`launchSettings.json`**
3. Verify or add profile:

```json
{
  "profiles": {
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

### Step 3: Configure AWS Credentials

**Option A: Using AWS CLI** (Recommended)

Open **Command Prompt** or **PowerShell**:
```powershell
aws configure
```

Enter your credentials:
```
AWS Access Key ID [None]: YOUR_ACCESS_KEY_FROM_CLOUDFORMATION
AWS Secret Access Key [None]: YOUR_SECRET_KEY_FROM_CLOUDFORMATION
Default region name [None]: us-east-1
Default output format [None]: json
```

**Option B: Using Environment Variables**

1. Right-click **`ReceiptProcessor`** project → **Properties**
2. Go to **Debug** → **General** → **Open debug launch profiles UI**
3. Add Environment Variables:
   - `AWS_ACCESS_KEY_ID` = Your access key
   - `AWS_SECRET_ACCESS_KEY` = Your secret key
   - `AWS_REGION` = us-east-1

---

## Building and Running

### Step 1: Build Solution

**Method A: Using Menu**
- Click **Build** → **Build Solution** (or press `Ctrl+Shift+B`)

**Method B: Using Solution Explorer**
- Right-click solution → **Build Solution**

**Expected Output in Output Window:**
```
Build started...
1>------ Build started: Project: ReceiptProcessor, Configuration: Debug Any CPU ------
1>ReceiptProcessor -> C:\Projects\ReceiptProcessorSolution\ReceiptProcessor\bin\Debug\net8.0\ReceiptProcessor.dll
========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========
```

### Step 2: Run Application

**Method A: Using Debug**
- Press **F5** or click **▶ Start Debugging** button

**Method B: Without Debug**
- Press **Ctrl+F5** or click **▶ Start Without Debugging**

### Step 3: Test the Application

1. Browser will automatically open to `https://localhost:5001`
2. You should see the Receipt Processor homepage
3. Try uploading a receipt image:
   - Click the upload area or drag-and-drop
   - Select a JPG or PNG image of a receipt
   - Wait for processing (5-15 seconds)
   - View extracted text and details

### Step 4: View Application Logs

**Output Window:**
- **View** → **Output** (or press `Ctrl+Alt+O`)
- Select **Show output from:** `Debug`

**Example logs:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

---

## Troubleshooting in Visual Studio

### Build Errors

**Error: "The type or namespace name 'Amazon' could not be found"**

**Solution:**
1. Right-click solution → **Restore NuGet Packages**
2. Clean solution: **Build** → **Clean Solution**
3. Rebuild: **Build** → **Rebuild Solution**

**Error: "Project file is incomplete"**

**Solution:**
1. Close Visual Studio
2. Delete `bin` and `obj` folders
3. Reopen solution
4. Build again

### Runtime Errors

**Error: "Unable to find AWS credentials"**

**Solution:**
1. Verify AWS CLI configuration:
   ```powershell
   aws configure list
   ```
2. Check appsettings.json has correct AWS configuration
3. Verify environment variables in Debug settings

**Error: "The specified bucket does not exist"**

**Solution:**
1. Verify CloudFormation stack is deployed
2. Update `S3BucketName` in appsettings.json with actual bucket name from CloudFormation outputs

### Debugging Tips

**Set Breakpoints:**
1. Click in left margin next to line number (red dot appears)
2. Run with F5
3. Application pauses at breakpoint

**View Variables:**
- Hover over variables while debugging
- Use **Locals** window: **Debug** → **Windows** → **Locals**
- Use **Watch** window: **Debug** → **Windows** → **Watch**

**Step Through Code:**
- **F10**: Step Over
- **F11**: Step Into
- **Shift+F11**: Step Out
- **F5**: Continue

---

## Additional Visual Studio Features

### IntelliSense

- Type `.` after object to see available methods
- Press `Ctrl+Space` for code completion
- Press `Ctrl+.` for quick actions and refactoring

### Code Snippets

- Type `prop` + `Tab` + `Tab` → Creates property
- Type `ctor` + `Tab` + `Tab` → Creates constructor
- Type `foreach` + `Tab` + `Tab` → Creates foreach loop

### Solution Explorer Shortcuts

- **Ctrl+;** → Search in Solution Explorer
- **F7** → View code
- **Shift+F7** → View designer

### Useful Extensions

1. **AWS Toolkit for Visual Studio**
   - **Tools** → **Extensions and Updates**
   - Search: "AWS Toolkit"
   - Install and restart Visual Studio
   - Access: **View** → **AWS Explorer**

---

## Next Steps

✅ Solution created and configured  
✅ NuGet packages installed  
✅ Code files added  
✅ AWS credentials configured  
✅ Application running successfully  

**Now proceed to:**
1. Deploy AWS infrastructure using CloudFormation (see main deployment guide)
2. Test receipt uploads
3. View extracted data in DynamoDB
4. Customize the application as needed

---

## Quick Reference

### Common Commands

| Action | Keyboard Shortcut |
|---|---|
| Build Solution | `Ctrl+Shift+B` |
| Start Debugging | `F5` |
| Start Without Debugging | `Ctrl+F5` |
| Stop Debugging | `Shift+F5` |
| Open Solution Explorer | `Ctrl+Alt+L` |
| Open Package Manager Console | `Ctrl+Q` → type "package" |
| Format Document | `Ctrl+K, Ctrl+D` |
| Comment/Uncomment | `Ctrl+K, Ctrl+C` / `Ctrl+K, Ctrl+U` |

### Project Files Checklist

- ✅ `ReceiptProcessor.sln` - Solution file
- ✅ `ReceiptProcessor.csproj` - Project file with NuGet packages
- ✅ `Program.cs` - Application entry point
- ✅ `appsettings.json` - Configuration
- ✅ `Controllers/HomeController.cs` - Controller
- ✅ `Models/Receipt.cs` - Data models
- ✅ `Services/IReceiptService.cs` - Service interface
- ✅ `Services/ReceiptService.cs` - Service implementation
- ✅ `Views/Home/Index.cshtml` - Home page
- ✅ `Views/Home/Details.cshtml` - Details page
- ✅ `Views/Shared/_Layout.cshtml` - Layout template

---

## Support Resources

- **Visual Studio Documentation**: [https://docs.microsoft.com/visualstudio/](https://docs.microsoft.com/visualstudio/)
- **ASP.NET Core Documentation**: [https://docs.microsoft.com/aspnet/core/](https://docs.microsoft.com/aspnet/core/)
- **AWS SDK for .NET**: [https://docs.aws.amazon.com/sdk-for-net/](https://docs.aws.amazon.com/sdk-for-net/)

---

**🎉 You're all set!** Your Receipt Processor application is ready to run in Visual Studio 2022.

# Deployment Guide
# Receipt Processor - Complete Deployment Guide
## AWS Free Tier ASP.NET Core Application

This guide will walk you through deploying a complete receipt processing system using AWS Free Tier services.

---

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [AWS Infrastructure Setup](#aws-infrastructure-setup)
3. [Application Setup](#application-setup)
4. [Testing the Application](#testing-the-application)
5. [Cost Management](#cost-management)
6. [Troubleshooting](#troubleshooting)

---

## Prerequisites

### Required Software
- **AWS CLI** (v2.x): [Download here](https://aws.amazon.com/cli/)
- **.NET 8.0 SDK**: [Download here](https://dotnet.microsoft.com/download)
- **Git**: [Download here](https://git-scm.com/)
- **Text Editor**: Visual Studio 2022, VS Code, or Rider

### AWS Account Requirements
- Active AWS Free Tier account
- IAM user with administrator access
- AWS CLI configured with credentials

---

## AWS Infrastructure Setup

### Step 1: Configure AWS CLI

```bash
# Configure AWS credentials
aws configure

# Enter your credentials when prompted:
# AWS Access Key ID: YOUR_ACCESS_KEY
# AWS Secret Access Key: YOUR_SECRET_KEY
# Default region name: us-east-1
# Default output format: json

# Verify configuration
aws sts get-caller-identity
```

### Step 2: Deploy CloudFormation Stack

```bash
# Create a new directory
mkdir receipt-processor-deployment
cd receipt-processor-deployment

# Save the CloudFormation template as 'infrastructure.yaml'
# (Use the CloudFormation template artifact provided)

# Deploy the stack
aws cloudformation create-stack \
  --stack-name receipt-processor-stack \
  --template-body file://infrastructure.yaml \
  --capabilities CAPABILITY_NAMED_IAM \
  --parameters ParameterKey=AppName,ParameterValue=receipt-processor

# Monitor stack creation (takes 3-5 minutes)
aws cloudformation describe-stacks \
  --stack-name receipt-processor-stack \
  --query 'Stacks[0].StackStatus'

# Wait until status is "CREATE_COMPLETE"
aws cloudformation wait stack-create-complete \
  --stack-name receipt-processor-stack
```

### Step 3: Retrieve Stack Outputs

```bash
# Get all outputs
aws cloudformation describe-stacks \
  --stack-name receipt-processor-stack \
  --query 'Stacks[0].Outputs'

# Save these values - you'll need them:
# - BucketName
# - DynamoDBTableName
# - ApiEndpoint
# - AccessKeyId
# - SecretAccessKey
# - Region
```

**Example Output:**
```json
[
  {
    "OutputKey": "BucketName",
    "OutputValue": "receipt-processor-receipts-123456789012"
  },
  {
    "OutputKey": "DynamoDBTableName",
    "OutputValue": "receipt-processor-receipts"
  },
  {
    "OutputKey": "AccessKeyId",
    "OutputValue": "AKIAIOSFODNN7EXAMPLE"
  },
  {
    "OutputKey": "SecretAccessKey",
    "OutputValue": "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY"
  }
]
```

**⚠️ IMPORTANT**: Save the `SecretAccessKey` immediately - it will not be displayed again!

---

## Application Setup

### Step 4: Create ASP.NET Core Project

```bash
# Create project directory
mkdir ReceiptProcessor
cd ReceiptProcessor

# Create new ASP.NET Core MVC project
dotnet new mvc -n ReceiptProcessor
cd ReceiptProcessor

# Add AWS SDK packages
dotnet add package AWSSDK.Core
dotnet add package AWSSDK.S3
dotnet add package AWSSDK.DynamoDBv2
dotnet add package AWSSDK.Rekognition
dotnet add package AWSSDK.Extensions.NETCore.Setup
```

### Step 5: Add Application Code

Create the following directory structure:
```
ReceiptProcessor/
├── Controllers/
├── Models/
├── Services/
├── Views/
│   ├── Home/
│   └── Shared/
└── wwwroot/
    └── css/
```

Copy the code from the artifacts:
1. **Program.cs** - Main application entry point
2. **Controllers/HomeController.cs** - Controller logic
3. **Models/Receipt.cs** - Data models
4. **Services/IReceiptService.cs** - Service interface
5. **Services/ReceiptService.cs** - Service implementation
6. **Views/** - All Razor views

### Step 6: Configure Application Settings

Edit `appsettings.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "AWS": {
    "Profile": "default",
    "Region": "us-east-1",
    "S3BucketName": "YOUR_BUCKET_NAME_FROM_STEP_3",
    "DynamoDBTableName": "receipt-processor-receipts"
  }
}
```

### Step 7: Configure AWS Credentials

**Option A: Using AWS CLI Configuration (Recommended for Development)**
```bash
# AWS CLI stores credentials in ~/.aws/credentials
# Already configured in Step 1
```

**Option B: Using Environment Variables (Recommended for Production)**
```bash
# Windows (PowerShell)
$env:AWS_ACCESS_KEY_ID="YOUR_ACCESS_KEY_FROM_STEP_3"
$env:AWS_SECRET_ACCESS_KEY="YOUR_SECRET_KEY_FROM_STEP_3"
$env:AWS_REGION="us-east-1"

# Linux/Mac
export AWS_ACCESS_KEY_ID="YOUR_ACCESS_KEY_FROM_STEP_3"
export AWS_SECRET_ACCESS_KEY="YOUR_SECRET_KEY_FROM_STEP_3"
export AWS_REGION="us-east-1"
```

### Step 8: Build and Run

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run

# Application will start on:
# https://localhost:5001
# http://localhost:5000
```

---

## Testing the Application

### Step 9: Test Receipt Upload

1. Open your browser to `https://localhost:5001`
2. You should see the Receipt Processor homepage
3. Click the upload area or drag-and-drop a receipt image (JPG/PNG)
4. Wait for processing (5-15 seconds)
5. View the extracted text and details

### Step 10: Verify AWS Resources

**Check S3 Bucket:**
```bash
aws s3 ls s3://YOUR_BUCKET_NAME/receipts/ --recursive
```

**Check DynamoDB Table:**
```bash
aws dynamodb scan --table-name receipt-processor-receipts
```

**Test API Gateway (Optional):**
```bash
# Get API endpoint from CloudFormation outputs
curl -X POST https://YOUR_API_ID.execute-api.us-east-1.amazonaws.com/prod/process \
  -H "Content-Type: application/json" \
  -d '{"receiptId":"test-123","s3Key":"receipts/2024/01/01/test.jpg"}'
```

---

## Cost Management

### AWS Free Tier Limits (12 months)

**S3:**
- 5 GB storage
- 20,000 GET requests
- 2,000 PUT requests

**DynamoDB:**
- 25 GB storage
- 25 read capacity units
- 25 write capacity units

**Lambda:**
- 1 million requests per month
- 400,000 GB-seconds compute time

**API Gateway:**
- 1 million API calls per month

**Rekognition:**
- 5,000 images per month (first year)

### Monitor Usage

```bash
# Check S3 bucket size
aws s3 ls s3://YOUR_BUCKET_NAME --recursive --summarize --human-readable

# Check DynamoDB table size
aws dynamodb describe-table \
  --table-name receipt-processor-receipts \
  --query 'Table.TableSizeBytes'

# Set up billing alerts in AWS Console:
# 1. Go to AWS Billing Dashboard
# 2. Click "Billing Preferences"
# 3. Enable "Receive Billing Alerts"
# 4. Create CloudWatch alarm for $1 threshold
```

### Clean Up Resources

When you're done testing:
```bash
# Delete all objects in S3 bucket first
aws s3 rm s3://YOUR_BUCKET_NAME --recursive

# Delete CloudFormation stack (removes all resources)
aws cloudformation delete-stack --stack-name receipt-processor-stack

# Verify deletion
aws cloudformation describe-stacks --stack-name receipt-processor-stack
```

---

## Troubleshooting

### Common Issues

**1. "Access Denied" Error**
```bash
# Verify IAM credentials
aws sts get-caller-identity

# Check IAM user permissions
aws iam list-attached-user-policies --user-name receipt-processor-app-user
```

**2. "Bucket not found" Error**
- Verify bucket name in `appsettings.json`
- Check CloudFormation outputs for correct bucket name
- Ensure bucket exists: `aws s3 ls`

**3. "Table not found" Error**
```bash
# List DynamoDB tables
aws dynamodb list-tables

# Verify table name matches configuration
```

**4. Rekognition "Invalid Image" Error**
- Ensure image format is JPEG or PNG
- Check image file size (max 5MB for S3 objects)
- Verify image is not corrupted

**5. Application Can't Connect to AWS**
```bash
# Test AWS connectivity
aws s3 ls

# Check environment variables
echo $AWS_ACCESS_KEY_ID
echo $AWS_REGION

# Verify credentials file
cat ~/.aws/credentials
```

### Enable Detailed Logging

Add to `appsettings.Development.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information",
      "Amazon": "Debug"
    }
  }
}
```

### Test Individual AWS Services

```csharp
// Add to Program.cs for testing
var s3Client = app.Services.GetRequiredService<IAmazonS3>();
var buckets = await s3Client.ListBucketsAsync();
Console.WriteLine($"Found {buckets.Buckets.Count} buckets");
```

---

## Production Deployment Considerations

### Security Best Practices

1. **Use IAM Roles instead of Access Keys**
   - For EC2: Attach IAM role to instance
   - For ECS/Fargate: Use task execution roles
   - For Lambda: Already using execution roles

2. **Enable S3 Bucket Encryption**
```bash
aws s3api put-bucket-encryption \
  --bucket YOUR_BUCKET_NAME \
  --server-side-encryption-configuration '{
    "Rules": [{
      "ApplyServerSideEncryptionByDefault": {
        "SSEAlgorithm": "AES256"
      }
    }]
  }'
```

3. **Enable CloudTrail for Auditing**
```bash
aws cloudtrail create-trail \
  --name receipt-processor-trail \
  --s3-bucket-name my-cloudtrail-bucket
```

4. **Use AWS Secrets Manager** (costs apply)
```bash
aws secretsmanager create-secret \
  --name receipt-processor/credentials \
  --secret-string '{"accessKey":"...","secretKey":"..."}'
```

### Scaling Considerations

- **DynamoDB**: Enable auto-scaling or use on-demand billing
- **S3**: Use lifecycle policies to move old receipts to Glacier
- **Lambda**: Increase memory/timeout if processing large images
- **API Gateway**: Enable caching to reduce Lambda invocations

---

## Additional Resources

- [AWS Free Tier Details](https://aws.amazon.com/free/)
- [AWS SDK for .NET Documentation](https://docs.aws.amazon.com/sdk-for-net/)
- [Amazon Rekognition Documentation](https://docs.aws.amazon.com/rekognition/)
- [DynamoDB Best Practices](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/best-practices.html)
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core/)

---

## Support

For issues or questions:
1. Check AWS Service Health Dashboard
2. Review CloudWatch Logs for Lambda functions
3. Check application logs: `dotnet run --verbosity detailed`
4. AWS Free Tier FAQ: https://aws.amazon.com/free/free-tier-faqs/

---

**🎉 Congratulations!** You now have a fully functional receipt processing system running on AWS Free Tier!
