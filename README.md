# clearkey-server
A license server that uses the W3C Clear Key license protocol.

# Key database
The license server will serve licenses for all keys stored in CPIX documents loaded from the App_Data directoryapplication. The documents must use the ".xml" file extension and must not store the content keys in an encrypted form.

The CPIX documents are loaded on application startup, so you must recycle the application to apply any changes.