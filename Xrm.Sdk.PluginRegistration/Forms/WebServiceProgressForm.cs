// =====================================================================
//
//  This file is part of the Microsoft Dynamics CRM SDK code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//
// =====================================================================

namespace Xrm.Sdk.PluginRegistration.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public partial class WebServiceProgressForm : Form
    {
        private ProgressIndicator m_progIndicator;
        private IWin32Window m_owner;

        public WebServiceProgressForm(IWin32Window owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            InitializeComponent();

            m_progIndicator = new ProgressIndicator(ProgressIndicatorInit, ProgressIndicatorComplete,
                null, ProgressIndicatorSetText, null, null);
            m_owner = owner;
        }

        public ProgressIndicator ProgressIndicator
        {
            get
            {
                return m_progIndicator;
            }
        }

        #region ProgressIndicator Implementation
        private void ProgressIndicatorInit(int min, int waitMilliseconds, int initialValue)
        {
            barRegistration.Minimum = min;
            barRegistration.Value = initialValue;
            barRegistration.Maximum = waitMilliseconds;

            tmrWait.Enabled = true;
        }

        private void ProgressIndicatorSetText(string text)
        {
            lblStatus.Text = text;

            ShowDialog(m_owner);
        }

        private void ProgressIndicatorComplete()
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion

        private void tmrWait_Tick(object sender, EventArgs e)
        {
            if (barRegistration.Value == barRegistration.Maximum)
            {
                DialogResult = DialogResult.Abort;
                Close();
            }
            else
            {
                barRegistration.Increment(tmrWait.Interval);
            }

            Application.DoEvents();
        }
    }
}
