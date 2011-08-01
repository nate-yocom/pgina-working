﻿using System;
using System.Collections.Generic;

namespace pGina.Shared.Interfaces
{    
    // All plugins must implement this interface
    public interface IPluginBase
    {
        string Name { get; }
        string Description { get; }
        string Version { get; }
        Guid Uuid { get; }
    }
    
    // Plugins which wish to integrate with the pGina configuration/Plugin
    // management UI must implement this interface
    public interface IPluginConfiguration : IPluginBase
    {
        void Configure();
    }
    
    // Plugins that want to be available for use in authentication must
    //  implement this interface.  At least one plugin
    //  must succeed for the login process to continue.
    public interface IPluginAuthentication : IPluginBase
    {
        Types.BooleanResult AuthenticateUser(Types.SessionProperties properties);
    }

    // Plugins that want to validate a users access (not identity per-se) must
    //  implement this interface.  All plugins which implement this interface
    //  must succeed for the login process to continue.
    public interface IPluginAuthorization : IPluginBase
    {
        Types.BooleanResult AuthorizeUser(Types.SessionProperties properties);
    }

    // Plugins that want to be involved in account management (post-auth*) 
    //  must implement this interface.  All plugins which implement this interface
    //  must succeed for the login process to continue.
    public interface IPluginAuthenticationGateway : IPluginBase
    {
        // User has been authenticated and authorized - now
        //  is your chance to do other accounting/management before the user's login is successful.        
        Types.BooleanResult AuthenticatedUserGateway(Types.SessionProperties properties);
    }
    
    // Plugins that want notification of events as they occur must implement
    // this interface.  Note that these are notifications only - these are
    // called from the core service in the context of the service and it's 
    // session (i.e. not in users session, as user, etc).  Plugins which want
    // to perform processing which requires specific context should see the 
    // IPlugin[User|System]SessionHelper interfaces.
    public interface IPluginEventNotifications : IPluginBase
    {
        // on login, logout, ... lock/disconnect etc?        
    }

    // Plugins that want to perform processing as the user should implement
    // this interface.  These plugins are loaded in the context of the users
    // session, as the user.  Note that users can stop (kill) the user session helper,
    // so *enforcement* does not belong here.
    public interface IPluginUserSessionHelper : IPluginBase
    {        
    }

    // Plugins that want to perform processing in the users session should implement
    // this interface.  These plugins are loaded in the context of the users
    // session, as the service account that runs the pGina service.  Non-admin
    // user's cannot stop this helper (admin users can).
    public interface IPluginSystemSessionHelper : IPluginBase
    {
    }
}