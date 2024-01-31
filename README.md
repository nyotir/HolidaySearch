# HolidaySearch


## Overview
HolidaySearch is a .NET class library project that provides a modular and pluggable architecture for holiday search functionality. It includes layers for data contracts, application logic, data access, and models. The library is designed to be flexible, allowing to replace the data layer with any preferred implementation.

## Project Structure

### HolidaySearch.Application

Contains the application layer logic and orchestrates data access.

### HolidaySearch.DataAccess

Pluggable data access layer.
The default implementation reads the data from JSON files.
It can be replaced with other data layers such as SQL or any custom implementation.

### HolidaySearch.Models

Defines the data models used throughout the application.
Encapsulates the structure of entities like Flights, Hotels etc.

### HolidaySearch.DataContracts

Defines the data contracts used for request and response.

### HolidaySearch.Tests

Includes unit tests

### HolidaySearch.API

Provides an API layer for quick end-to-end integration testing. It also demonstrates how the library can be consumed in a real-world scenario.

## To replace the data layer

Create a new data layer implementing the IDataReader interface.

## Caching

Caching mechanism can be integrated into the application layer particularly to cache the airport data, as it is accessed frequently.
Optionally, configuration options can be provided for enabling/disabling caching.

## Important
The implementation is designed to be asynchronous end-to-end, emphasizing that all database calls should be asynchronous. However, in the current scenario where data is small and retrieved from JSON, the use of asynchronous operations may not yield significant benefits.
Also, in the original data provided, there are duplicate records for hotels. These are not changed. So final output will contain some duplicate data. 
e.g.
  {
    "id": 3,
    "name": "Sol Katmandu Park & Resort",
    "arrival_date": "2023-06-15",
    "price_per_night": 59,
    "local_airports": [ "PMI" ],
    "nights": 14
  },
  {
    "id": 4,
    "name": "Sol Katmandu Park & Resort",
    "arrival_date": "2023-06-15",
    "price_per_night": 59,
    "local_airports": [ "PMI" ],
    "nights": 14
  }
Logic to avoid duplicate data is not implemented.

## Getting Started
To use the HolidaySearch library in your project:

Reference the HolidaySearch assembly in your project and call method _searchService.SearchHoliday(). Sample implementation is shown in API project.
