﻿using System;

using Health.Direct.Config.Client.DomainManager;
using Health.Direct.Config.Store;

namespace AdminUI.Logic.Views
{
    public partial class NewAddressControl : System.Web.UI.UserControl
    {
        #region "Public properties with ViewState as the backing store"
        public string Owner
        {
            get
            {
                if (ViewState["Owner"] != null)
                {
                    return ViewState["Owner"] as string;
                }
                else
                {
                    return String.Empty;
                }
            }
            set { ViewState["Owner"] = value; }
        }
        public long DomainId
        {
            get
            {
                long returnValue = -1;
                if (ViewState["DomainId"] != null)
                {

                    long.TryParse(ViewState["DomainId"].ToString(), out returnValue);

                }
                return returnValue;
            }
            set { ViewState["DomainId"] = value; }
        }
        #endregion

        private Health.Direct.Config.Client.DomainManager.AddressManagerClient _addressManagerClient;
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.Visible = !string.IsNullOrEmpty(Owner) && DomainId > 0;
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            CreateNewDomain();
            ClearContent();
        }

        private void ClearContent()
        {
            EmailAddressTextBox.Text = string.Empty;
            DisplayNameTextBox.Text = string.Empty;
            TypeTextBox.Text = string.Empty;
        }

        private void CreateNewDomain()
        {
            _addressManagerClient = new AddressManagerClient();

            if (!string.IsNullOrEmpty(Owner) && DomainId > 0)
            {
                
                try {
                _addressManagerClient.AddAddresses
                    (
                        new Address[]
                            {
                                new Address(
                                    DomainId, 
                                    EmailAddressTextBox.Text,
                                    DisplayNameTextBox.Text)
                            }
                    );
                    }
                catch (System.ServiceModel.FaultException<Health.Direct.Config.Store.ConfigStoreFault> ex)
                {
                    ErrorLiteral.Text = "The address already exists.";
                }
            }
        }
    }
}