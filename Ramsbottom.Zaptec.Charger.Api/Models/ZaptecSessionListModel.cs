using System;
using System.Collections.Generic;

namespace NeilRamsbottom.Zaptec.Charger.Api.Models
{

    public class ZaptecSessionListModel
    {
        [Obsolete("Property is deprecated and an alias for {Zaptec.ZapCloud.WebAPI.Models.Sessions.SessionModel.UserEmail}. Please use {Zaptec.ZapCloud.WebAPI.Models.Sessions.SessionModel.UserEmail} instead.")]
        public string UserUserName { get; set; }
        public string Id { get; set; }
        public string DeviceId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double Energy { get; set; }
        public int CommitMetaData { get; set; }
        public DateTime CommitEndDateTime { get; set; }
        public string UserFullName { get;set; }
        public string ChargerId { get; set; }
        public string DeviceName { get; set; }
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public string TokenName { get; set; }
        public string ExternalId { get; set; }
        public bool ExternallyEnded { get; set; }
        public List<ZaptecSessionEnergyDetailsModel> EnergyDetails { get; set; }
        public ZaptecVersion ChargerFirmwareVersion { get; set; }
        public string SignedSession { get; set; }
        public string ReplacedBySessionId { get; set; }

        public override string ToString()
        {
            return $"{Energy} kWh, {StartDateTime:dd.MM}, {TokenName}";
        }
    }
}
