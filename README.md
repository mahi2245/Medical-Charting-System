# README

Medical Charting System

COP4870 Final Project

Mahi Agrawal - ma23k



This project is a simple medical charting application using .NET MAUI, WebAPI, and JSON file persistence. The system supports managing patients, physicians, and appointments, with full patient CRUD backend by a REST API.



## Running the Project

1. Start the WebAPI
dotnet run --project API.Assignment1/API.Assignment1.csproj


API runs at: http://localhost:5180/Patient


2. Run the MAUI App

Use VS Code Run to start MAUI app


Patient data is saved and loaded, and it can be viewed at http://localhost:5180/Patient


## Features

Patient Management (CRUD) - Name, address, birthdate, race, gender

* Fully integrated with WebAPI

* Data persists in patients.json

Physician Management (CRUD)

Appointment Scheduling (CRUD)

* Prevents double booking

* Only allows appointments Mon–Fri, 8AM–5PM

AppShell navigation across Patients, Physicians, and Appointments

Search for patients via API

Inline patient and physician add UI

