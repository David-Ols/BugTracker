# BugTracker

BugTracker is web application for tracking bugs.
It uses VUEjs on the presentation layer, and backend is done on .Net 6.

## Frontend Setup

Navigate to Frontend folder and run commands:

```npm
npm install
```

Compiles and hot-reloads for development
```npm
npm run serve
```

Application will run at http://localhost:8080/


## Backend Setup

Navigate to Backend folder and open BugTracker.sln.

This solution will load backend, UserAPI, BugAPI and UnitTests projects.

Running this solution will start 3 separate projects, backend server and 2 APIs. 


The application is not running and you can test it at http://localhost:8080/

## Additional Notes

In case you need to change ports for any project, here are locations where to do find it:

### Update ports for APIs
```
Backend/appsettings.Development.json
```

### Update port for Frontend
```
Backend/Program.cs
```
