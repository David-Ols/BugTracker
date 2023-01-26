# BugTracker

BugTracker is web application for tracking bugs.
It uses VUEjs on the presentation layer, and backend is built on .Net 6.

## Frontend Setup

Navigate to Frontend folder and run commands:

```npm
npm install
```

Compiles and hot-reloads for development
```npm
npm run serve
```

Application will run at http://localhost:8080 or http://192.168.0.26:8080


## Backend Setup

Navigate to Backend folder and open BugTracker.sln.

This solution will load backend, UserAPI, BugAPI, UnitTests projects and class libraries.

Ensure that this solution will start 3 separate projects, backend server(BugTracker), BugAPI and UserAPI. 


The application is now running and you can test it at http://localhost:8080 or http://192.168.0.26:8080

## Additional Notes

In case you need to change ports for any project, here are locations where to find it:

### Update ports for APIs
```
Backend/appsettings.Development.json
```

### Update port for Frontend
Use both Local and Network addresses provided to you after npm run serve command on Frontend.
```
Backend/Program.cs
```
