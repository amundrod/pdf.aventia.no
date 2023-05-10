Aventia PDF Search API

This API is a PDF search service developed for Aventia as part of a Bachelor's project. It provides a mechanism for indexing and searching PDF documents for specific keywords.

Features

Indexing of PDF files into a MySQL database.
Full-text search of PDF files.
Highlights the search keyword in the search results.

Technology Stack

ASP.NET Core 5.0
MySQL
Hangfire
IronPDF

Prerequisites

Before you begin, ensure you have installed the following:

.NET 5.0 SDK
MySQL Server
An IDE or text editor of your choice (e.g. Visual Studio, Visual Studio Code)

Setup

Clone the repository to your local machine.

Navigate to the root directory of the project and locate the appsettings.json file. Update the connection string to point to your MySQL server instance.

Navigate to the GlobalSettings.cs file under the pdf.aventia.no namespace and set the DefaultFolderPath to the directory containing your PDF files.

Run the SQL script located in the root directory of the project to create the necessary database table.

Open a terminal window and navigate to the root directory of the project. Run the command dotnet run to start the application.

The application will now be running and accessible at https://localhost:5001.


Usage

Index all PDFs - Send a GET request to https://localhost:5001/api/pdf/index. This will index all PDFs in the specified directory.

Search PDFs - Send a GET request to https://localhost:5001/api/pdf/search?word={your_keyword}. This will search all indexed PDFs for the specified keyword and return the ID of the PDF and the sentences containing the keyword.


License

This project is licensed under the MIT License.
