// This file is part of the HistoryMindLernen Project
//
// Copyright (C) 2022
//
// “Commons Clause” License Condition v1.0
// The Software is provided to you by the Licensor under the License, as defined below, subject to the following condition.
//
// Without limiting other conditions in the License, the grant of rights under the License will not include,
// and the License does not grant to you,the right to Sell the Software.
// For purposes of the foregoing, “Sell” means practicing any or all of the rights granted to you under the License to provide to third parties,
// for a fee or other consideration (including without limitation fees for hosting or consulting/ support services related to the Software),
// a product or service whose value derives, entirely or substantially, from the functionality of the Software.
//
// Any license notice or attribution required by the License must also include this Commons Clause License Condition notice.
//
// Software: HistoryMindLernen
// License: AGPL v3.0
// Licensor: Frantisek Pis

using Foundation;
using UIKit;

namespace HistoryMindLernen.Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}