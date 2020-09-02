// Decompiled with JetBrains decompiler
// Type: Microsoft.Diagnostics.Telemetry.Internal.PartA_PrivTags
// Assembly: MitaBroker, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04F1F240-3A91-44F5-8C6F-E562756B4D74
// Assembly location: C:\Program Files (x86)\Windows Application Driver\MitaBroker.dll

using System;

namespace Microsoft.Diagnostics.Telemetry.Internal {
    [Flags]
    internal enum PartA_PrivTags : ulong {
        None = 0,
        Account = 1,
        BrowsingHistory = 2,
        CloudServiceProvider = 4,
        CommuteAndTravel = 8,
        CompensationAndBenefits = 16, // 0x0000000000000010
        ContentConsumption = 32, // 0x0000000000000020
        Credentials = 64, // 0x0000000000000040
        CustomerContact = 128, // 0x0000000000000080
        CustomerContactList = 256, // 0x0000000000000100
        CustomerContent = 512, // 0x0000000000000200
        DemographicInformation = 1024, // 0x0000000000000400
        DeviceConnectivityAndConfiguration = 2048, // 0x0000000000000800
        EnvironmentalSensor = 4096, // 0x0000000000001000
        EUII = 8192, // 0x0000000000002000
        FeedbackAndRatings = 16384, // 0x0000000000004000
        FinancialAccountingAndRecords = 32768, // 0x0000000000008000
        FitnessAndActivity = 65536, // 0x0000000000010000
        InkingTypingAndSpeechUtterance = 131072, // 0x0000000000020000
        InterestsAndFavorites = 262144, // 0x0000000000040000
        LearningAndDevelopment = 524288, // 0x0000000000080000
        LicensingAndPurchase = 1048576, // 0x0000000000100000
        MicrosoftCommunications = 2097152, // 0x0000000000200000
        PaymentInstrument = 4194304, // 0x0000000000400000
        PreciseUserLocation = 8388608, // 0x0000000000800000
        ProductAndServicePerformance = 16777216, // 0x0000000001000000
        ProductAndServiceUsage = 33554432, // 0x0000000002000000
        ProfessionalAndPersonalProfile = 67108864, // 0x0000000004000000
        PublicPersonal = 134217728, // 0x0000000008000000
        Recruitment = 268435456, // 0x0000000010000000
        SearchRequestsAndQuery = 536870912, // 0x0000000020000000
        Social = 1073741824, // 0x0000000040000000
        SoftwareSetupAndInventory = 2147483648, // 0x0000000080000000
        Support = 4294967296, // 0x0000000100000000
        SupportContent = 8589934592, // 0x0000000200000000
        SupportInteraction = 17179869184, // 0x0000000400000000
        WorkContracts = 34359738368, // 0x0000000800000000
        WorkplaceInteractions = 68719476736, // 0x0000001000000000
        WorkProfile = 137438953472, // 0x0000002000000000
        WorkRecognition = 274877906944, // 0x0000004000000000
        WorkTime = 549755813888 // 0x0000008000000000
    }
}