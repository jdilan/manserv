/*
================================================================================
MANSERV Loan Account Management System - Local Testing
Mock Configuration Manager
================================================================================
Purpose: Manage mock service configuration from JSON file
Author: System Generated
Date: December 6, 2025
================================================================================
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ManservLoanSystem.LocalTesting.Mocks
{
    /// <summary>
    /// Manages configuration for mock services
    /// Loads settings from MockConfig.json
    /// </summary>
    public class MockConfigManager
    {
        private static MockConfigManager _instance;
        private static readonly object _lock = new object();
        
        private MockConfiguration _config;
        private readonly string _configPath;

        private MockConfigManager()
        {
            _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mocks", "MockConfig.json");
            LoadConfiguration();
        }

        public static MockConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MockConfigManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public MockConfiguration Config => _config;

        private void LoadConfiguration()
        {
            try
            {
                if (File.Exists(_configPath))
                {
                    var json = File.ReadAllText(_configPath);
                    _config = JsonSerializer.Deserialize<MockConfiguration>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    // Use default configuration if file doesn't exist
                    _config = GetDefaultConfiguration();
                    SaveConfiguration();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading mock configuration: {ex.Message}");
                _config = GetDefaultConfiguration();
            }
        }

        public void SaveConfiguration()
        {
            try
            {
                var json = JsonSerializer.Serialize(_config, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                
                var directory = Path.GetDirectoryName(_configPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                File.WriteAllText(_configPath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving mock configuration: {ex.Message}");
            }
        }

        public void ReloadConfiguration()
        {
            LoadConfiguration();
        }

        private MockConfiguration GetDefaultConfiguration()
        {
            return new MockConfiguration
            {
                MockMode = "lenient",
                Validation = new ValidationConfig
                {
                    StrictMode = false,
                    AllowDuplicateRefNo = true,
                    EnforceAllBusinessRules = false
                },
                Users = new List<UserConfig>
                {
                    new UserConfig
                    {
                        UserId = "user1",
                        Username = "John User",
                        Role = "User",
                        Centers = new List<string> { "01" }
                    },
                    new UserConfig
                    {
                        UserId = "admin1",
                        Username = "Admin User",
                        Role = "Administrator",
                        Centers = new List<string> { "01", "02", "03" }
                    }
                },
                TestScenarios = new TestScenariosConfig
                {
                    HappyPath = new ScenarioConfig { Enabled = true },
                    ValidationFailures = new ScenarioConfig { Enabled = false },
                    PermissionDenied = new ScenarioConfig { Enabled = false },
                    ExternalServiceFailures = new ScenarioConfig { Enabled = false }
                },
                Logging = new LoggingConfig
                {
                    LogToConsole = true,
                    LogToFile = false,
                    LogLevel = "Info"
                }
            };
        }
    }

    #region Configuration Classes

    public class MockConfiguration
    {
        public string MockMode { get; set; }
        public string Description { get; set; }
        public ValidationConfig Validation { get; set; }
        public List<UserConfig> Users { get; set; }
        public TestScenariosConfig TestScenarios { get; set; }
        public LoggingConfig Logging { get; set; }
    }

    public class ValidationConfig
    {
        public bool StrictMode { get; set; }
        public bool AllowDuplicateRefNo { get; set; }
        public bool EnforceAllBusinessRules { get; set; }
    }

    public class UserConfig
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public List<string> Centers { get; set; }
        public string Description { get; set; }
    }

    public class TestScenariosConfig
    {
        public ScenarioConfig HappyPath { get; set; }
        public ScenarioConfig ValidationFailures { get; set; }
        public ScenarioConfig PermissionDenied { get; set; }
        public ScenarioConfig ExternalServiceFailures { get; set; }
    }

    public class ScenarioConfig
    {
        public bool Enabled { get; set; }
        public string Description { get; set; }
    }

    public class LoggingConfig
    {
        public bool LogToConsole { get; set; }
        public bool LogToFile { get; set; }
        public string LogLevel { get; set; }
    }

    #endregion
}
