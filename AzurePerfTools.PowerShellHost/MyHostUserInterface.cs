namespace AzurePerfTools.PowerShellHost
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Management.Automation;
    using System.Management.Automation.Host;
    using System.Text;

    /// <summary>
    /// A sample implementation of the PSHostUserInterface abstract class for 
    /// console applications. Not all members are implemented. Those that are 
    /// not implemented throw a NotImplementedException exception or return 
    /// nothing. Members that are implemented include those that map easily to 
    /// Console APIs and a basic implementation of the prompt API provided. 
    /// </summary>
    internal class MyHostUserInterface : PSHostUserInterface
    {
        /// <summary>
        /// A reference to the PSRawUserInterface implementation.
        /// </summary>
        private MyRawUserInterface myRawUi = new MyRawUserInterface();

        private StringBuilder currentOutput;

        public MyHostUserInterface(StringBuilder output) : base()
        {
            this.currentOutput = output;
        }

        /// <summary>
        /// Gets an instance of the PSRawUserInterface object for this host
        /// application.
        /// </summary>
        public override PSHostRawUserInterface RawUI
        {
            get { return this.myRawUi; }
        }

        /// <summary>
        /// Prompts the user for input. 
        /// <param name="caption">The caption or title of the prompt.</param>
        /// <param name="message">The text of the prompt.</param>
        /// <param name="descriptions">A collection of FieldDescription objects  
        /// that describe each field of the prompt.</param>
        /// <returns>A dictionary object that contains the results of the user 
        /// prompts.</returns>
        public override Dictionary<string, PSObject> Prompt(
                                  string caption,
                                  string message,
                                  Collection<FieldDescription> descriptions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provides a set of choices that enable the user to choose a 
        /// single option from a set of options. 
        /// </summary>
        /// <param name="caption">Text that proceeds (a title) the choices.</param>
        /// <param name="message">A message that describes the choice.</param>
        /// <param name="choices">A collection of ChoiceDescription objects that  
        /// describ each choice.</param>
        /// <param name="defaultChoice">The index of the label in the Choices  
        /// parameter collection. To indicate no default choice, set to -1.</param>
        /// <returns>The index of the Choices parameter collection element that 
        /// corresponds to the option that is selected by the user.</returns>
        public override int PromptForChoice(
                                            string caption,
                                            string message,
                                            Collection<ChoiceDescription> choices,
                                            int defaultChoice)
        {
            throw new NotImplementedException();
        }

        #region Not Implemented

        /// <summary>
        /// Prompts the user for credentials with a specified prompt window 
        /// caption, prompt message, user name, and target name. In this 
        /// example this functionality is not needed so the method throws a 
        /// NotImplementException exception.
        /// </summary>
        /// <param name="caption">The caption for the message window.</param>
        /// <param name="message">The text of the message.</param>
        /// <param name="userName">The user name whose credential is to be 
        /// prompted for.</param>
        /// <param name="targetName">The name of the target for which the 
        /// credential is collected.</param>
        /// <returns>Throws a NotImplementedException exception.</returns>
        public override PSCredential PromptForCredential(
                                                         string caption,
                                                         string message,
                                                         string userName,
                                                         string targetName)
        {
            throw new NotImplementedException(
                                 "The method or operation is not implemented.");
        }

        /// <summary>
        /// Prompts the user for credentials by using a specified prompt window 
        /// caption, prompt message, user name and target name, credential 
        /// types allowed to be returned, and UI behavior options. In this 
        /// example this functionality is not needed so the method throws a 
        /// NotImplementException exception.
        /// </summary>
        /// <param name="caption">The caption for the message window.</param>
        /// <param name="message">The text of the message.</param>
        /// <param name="userName">The user name whose credential is to be 
        /// prompted for.</param>
        /// <param name="targetName">The name of the target for which the 
        /// credential is collected.</param>
        /// <param name="allowedCredentialTypes">A PSCredentialTypes constant  
        /// that identifies the type of credentials that can be returned.</param>
        /// <param name="options">A PSCredentialUIOptions constant that 
        /// identifies the UI behavior when it gathers the credentials.</param>
        /// <returns>Throws a NotImplementedException exception.</returns>
        public override PSCredential PromptForCredential(
                                           string caption,
                                           string message,
                                           string userName,
                                           string targetName,
                                           PSCredentialTypes allowedCredentialTypes,
                                           PSCredentialUIOptions options)
        {
            throw new NotImplementedException(
                                    "The method or operation is not implemented.");
        }

        /// <summary>
        /// Reads characters entered by the user until a newline (carriage return) 
        /// is encountered and returns the characters as a secure string. In this 
        /// example this functionality is not needed so the method throws a 
        /// NotImplementException exception.
        /// </summary>
        /// <returns>Throws a NotImplemented exception.</returns>
        public override System.Security.SecureString ReadLineAsSecureString()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        #endregion

        /// <summary>
        /// Reads characters that are entered by the user until a newline 
        /// (carriage return) is encountered.
        /// </summary>
        /// <returns>The characters that are entered by the user.</returns>
        public override string ReadLine()
        {
            //return Console.ReadLine();
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes characters to the output display of the host.
        /// </summary>
        /// <param name="value">The characters to be written.</param>
        public override void Write(string value)
        {
            this.currentOutput.Append(value);
        }

        /// <summary>
        /// Writes characters to the output display of the host with possible 
        /// foreground and background colors. 
        /// </summary>
        /// <param name="foregroundColor">The color of the characters.</param>
        /// <param name="backgroundColor">The backgound color to use.</param>
        /// <param name="value">The characters to be written.</param>
        public override void Write(
                                   ConsoleColor foregroundColor,
                                   ConsoleColor backgroundColor,
                                   string value)
        {
            this.currentOutput.Append(value);
        }


        /// <summary>
        /// Writes a line of characters to the output display of the host 
        /// with foreground and background colors and appends a newline (carriage return). 
        /// </summary>
        /// <param name="foregroundColor">The forground color of the display. </param>
        /// <param name="backgroundColor">The background color of the display. </param>
        /// <param name="value">The line to be written.</param>
        public override void WriteLine(
                                       ConsoleColor foregroundColor,
                                       ConsoleColor backgroundColor,
                                       string value)
        {
            this.currentOutput.AppendLine(value);
        }

        /// <summary>
        /// Writes a debug message to the output display of the host.
        /// </summary>
        /// <param name="message">The debug message that is displayed.</param>
        public override void WriteDebugLine(string message)
        {
            this.WriteLine(
                           ConsoleColor.DarkYellow,
                           ConsoleColor.Black,
                           String.Format(CultureInfo.CurrentCulture, "DEBUG: {0}", message));
        }

        /// <summary>
        /// Writes an error message to the output display of the host.
        /// </summary>
        /// <param name="value">The error message that is displayed.</param>
        public override void WriteErrorLine(string value)
        {
            this.WriteLine(
                           ConsoleColor.Red,
                           ConsoleColor.Black,
                           value);
        }

        /// <summary>
        /// Writes a newline character (carriage return) 
        /// to the output display of the host. 
        /// </summary>
        public override void WriteLine()
        {
            this.currentOutput.AppendLine();
        }

        /// <summary>
        /// Writes a line of characters to the output display of the host 
        /// and appends a newline character(carriage return). 
        /// </summary>
        /// <param name="value">The line to be written.</param>
        public override void WriteLine(string value)
        {
            this.currentOutput.AppendLine(value);
        }

        /// <summary>
        /// Writes a progress report to the output display of the host.
        /// </summary>
        /// <param name="sourceId">Unique identifier of the source of the record. </param>
        /// <param name="record">A ProgressReport object.</param>
        public override void WriteProgress(long sourceId, ProgressRecord record)
        {

        }

        /// <summary>
        /// Writes a verbose message to the output display of the host.
        /// </summary>
        /// <param name="message">The verbose message that is displayed.</param>
        public override void WriteVerboseLine(string message)
        {
            this.WriteLine(
                           ConsoleColor.Green,
                           ConsoleColor.Black,
                           String.Format(CultureInfo.CurrentCulture, "VERBOSE: {0}", message));
        }

        /// <summary>
        /// Writes a warning message to the output display of the host.
        /// </summary>
        /// <param name="message">The warning message that is displayed.</param>
        public override void WriteWarningLine(string message)
        {
            this.WriteLine(
                           ConsoleColor.Yellow,
                           ConsoleColor.Black,
                           String.Format(CultureInfo.CurrentCulture, "WARNING: {0}", message));
        }
    }
}