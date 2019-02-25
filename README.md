PdfSplit is a .NET console application for splitting PDF files into multiple files. Pass a PDF file to this app on the command line (or drag a PDF file onto the executable in Windows Explorer), and it will be split into multiple documents, one document per page.

The project uses the open source PDFsharp library, which can be downloaded from http://sourceforge.net/projects/pdfsharp/

Note: It was developed in Microsoft Visual Studio 2008 (although it currently needs Visual Studio 2017 to build), and targeting the .NET Framework 2.0. This is because the lady who I originally developed it for, to use at work, only has .NET 2.0 on her PC and the company policy didn't permit her to upgrade. Hence it needs to be left targetting that framework version.