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

using HistoryMindLernen.Mobile.Database;
using HistoryMindLernen.Mobile.Droid;
using Microsoft.Data.Sqlite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(Database))]

namespace HistoryMindLernen.Mobile.Droid
{
    public class Database : ISQLite
    {
        SqliteConnection ISQLite.GetConnection()
        {
            const string databaseName = "HistoryMind.db";
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbFile = Path.Combine(docFolder, databaseName); // FILE NAME TO USE WHEN COPIED

            FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
            Android.App.Application.Context.Assets.Open(databaseName).CopyTo(writeStream);

            return new SqliteConnection($@"Data Source={dbFile};Mode=ReadOnly;");
        }
    }
}