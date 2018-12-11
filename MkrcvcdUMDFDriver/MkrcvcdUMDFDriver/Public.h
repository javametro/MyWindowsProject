/*++

Module Name:

    public.h

Abstract:

    This module contains the common declarations shared by driver
    and user applications.

Environment:

    driver and application

--*/

//
// Define an Interface Guid so that apps can find the device and talk to it.
//

DEFINE_GUID (GUID_DEVINTERFACE_MkrcvcdUMDFDriver,
    0xb9de95de,0x19e1,0x46a0,0xb2,0x5b,0xbc,0xb5,0x4f,0xef,0x70,0x09);
// {b9de95de-19e1-46a0-b25b-bcb54fef7009}
