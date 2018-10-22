# Imdex Technical Test

This is an ASP.netCore application serving an Angular front end.

# Requirements

In order to run the application, you need to have installed the following:

- Visual Studio 2017

- IISExpress

- Node.JS

# Running the Application

Clone the repository and open the solution file in Visual Studio.

In the debug configuration, run the application using the IISExpress profile.

The first time you build the application might take a long time due to building the node environment.

Once the application starts, it should automatically connect to the Google cloud SQL server at 35.201.3.173.

Your default web browser should now automatically open and show you the web page.

# Common Issues

- My web browser displays an error message saying something like "npm script failed to start"

The node packages have failed to build, and you need to open a node command prompt, navigate to the /Imdex/ClientApp directory and run the command "npm install"

- The web browser shows a "loading...." message underneath the "Imdex" header

Check your internet connection. The application needs to connect to a google cloud sql instance in order to retrieve data.
