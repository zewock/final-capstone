﻿using Capstone.DAO;
using Capstone.Models.DatabaseModles;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TEbucksServer.DAO;
using Capstone.Models;
using TEBucksServer.DAO;
using TEBucksServer.Security;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using Capstone.Models.IncomingDTOs;

namespace Capstone.Controllers
{
    [Route("")]
    [ApiController]
    public class UserController
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserDao userDao;
        private readonly IForumDao forumDao;

        public LoginController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher, IUserDao _userDao)
        {
            tokenGenerator = _tokenGenerator;
            passwordHasher = _passwordHasher;
            userDao = _userDao;
        }
    }
}