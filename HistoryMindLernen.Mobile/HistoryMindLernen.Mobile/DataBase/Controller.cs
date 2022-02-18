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

using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace HistoryMindLernen.Mobile.Database
{
    public interface ISQLite
    {
        SqliteConnection GetConnection();
    }

    public class Controller
    {
        public static Controller Instance { get; set; }
        private SqliteConnection SQLiteConnection { get; set; }

        public struct HistoryMindResult
        {
            public string Begriff { get; set; }
            public string Erklärung { get; set; }
            public long Confidence { get; set; }
        }

        private Controller()
        {
            SQLiteConnection = DependencyService.Get<ISQLite>().GetConnection();
        }

        public static Controller GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Controller();
            }

            return Instance;
        }

        public HistoryMindResult RandomHistoryMind(bool historyMind1 = true, bool historyMind2 = true, bool historyMind3 = true)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (historyMind1)
            {
                stringBuilder.AppendLine("SELECT * FROM HistoryMind");

                if (historyMind2 || historyMind3)
                {
                    stringBuilder.AppendLine("UNION");
                }
            }
            if (historyMind2)
            {
                stringBuilder.AppendLine("SELECT * FROM HistoryMind2");

                if (historyMind3)
                {
                    stringBuilder.AppendLine("UNION");
                }
            }
            if (historyMind3)
            {
                stringBuilder.AppendLine("SELECT * FROM HistoryMind3");
            }

            IEnumerable<HistoryMindResult> query = SQLiteConnection.Query<HistoryMindResult>(stringBuilder.ToString());

            return query.ElementAt(new Random().Next(query.Count()));
        }
    }
}