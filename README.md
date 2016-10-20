# clearkey-server
A license server that uses the [W3C Clear Key](https://www.w3.org/TR/encrypted-media/#clear-key) license protocol.

# Installation

This is a standard ASP.NET web application and can be deployed using ordinary .NET web application deployment patterns (compile the code and copy the files over to the web server).

System requirements:

* .NET Framework 4.6.2 or newer.

After deployment, you must add your content keys to the key database (see below).

The application exposes two web API endpoints:

* POST /AcquireLicense - will return a license response for any license request that requests one or more content keys that are present in the key database.
* GET /Diagnostics - will list the IDs of the keys stored in the key database. For manual diagnostics purposes only.

# Key database
The license server will serve licenses for all keys stored in [CPIX documents](http://dashif.org/guidelines/) loaded from the App_Data directory. The following requirements apply:

* The CPIX documents must use the ".xml" file extension.
* The keys in the CPIX documents must not be encrypted.

The key database is loaded on application startup and cached. You must recycle the application for any changes to take effect.