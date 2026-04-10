using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PsigenVision.Utilities.Testing;

namespace PsigenVision.Utilities.Collection.Examples
{
    public class SortedListTest : TestBaseClass
    {
        protected SortedList<int, List<TestClass>> sortedListOfLists = new();
        protected SortedList<int, List<TestClass>> cachedSortedListOfLists = new();

        #region Simulation Steps

        protected override void InitializeSteps()
        {
            simulationSteps.Add(TestContains);
            simulationSteps.Add(TestSafeContains);
            simulationSteps.Add(TestAddOrAppend);
            simulationSteps.Add(TestSafeAddOrAppend);
            simulationSteps.Add(TestAddOrAppendUniqueWithoutReplacing);
            simulationSteps.Add(TestAddOrAppendUniqueWithReplacing);
            simulationSteps.Add(TestSafeAddOrAppendUniqueWithoutReplacing);
            simulationSteps.Add(TestSafeAddOrAppendUniqueWithReplacing);
            simulationSteps.Add(TestRemove);
            simulationSteps.Add(TestThoroughRemove);
            simulationSteps.Add(TestTryRemove);
            simulationSteps.Add(TestTryThoroughRemove);
            simulationSteps.Add(TestTrySafeRemove);
            simulationSteps.Add(TestTryThoroughSafeRemove);
            simulationSteps.Add(TestFormat);
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                PrintSortedList();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PrintSortedList(cachedSortedListOfLists,
                    testClass => (testClass.name == "") ? "empty string" : testClass.name);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
                TestFormat();
        }

        private void TestContains()
        {
            Debug.Log("Testing Contains. \n" +
                      "This extension checks for the presence of a specific key-value pair exists where the value is contained within the list associated with the key. " +
                      "This extension DOES NOT check that the list associated with the key is NOT null.");
            PrintSortedList();
            Debug.Log($"Checking if a specific key-value (0,one) is contained using Contains");
            Debug.Log(" Check reports " +
                      $"{sortedListOfLists.Contains(0, new TestClass("one"))}");

            Debug.Log($"Checking if a specific key-value (1,two) is contained using Contains");
            Debug.Log(" Check reports " +
                      $"{sortedListOfLists.Contains(1, new TestClass("two"))}");

            Debug.Log(
                $"Checking if a specific key-value (2,two) is contained using Contains. Note: the list is empty.");
            Debug.Log(" Check reports " +
                      $"{sortedListOfLists.Contains(2, new TestClass("two"))}");

            Debug.Log($"Checking if a specific key-value (3,two) is contained using Contains. Note: the list is null.");
            string report;
            try
            {
                report = sortedListOfLists.Contains(3, new TestClass("two")).ToString();
            }
            catch (Exception e)
            {
                report = e.ToString();
            }

            Debug.Log($" Check reports {report}");
            Debug.Log(
                "The previous check should have thrown an error, since there is no null-check for the value-list in the Contains extension.");
        }

        private void TestSafeContains()
        {
            Debug.Log("Testing SafeContains. \n" +
                      "This extension checks for the presence of a specific key-value pair exists where the value is contained within the list associated with the key. " +
                      "This extension DOES NOT check that the list associated with the key is NOT null.");
            PrintSortedList();
            Debug.Log($"Checking if a specific key-value (0,one) is contained using SafeContains");
            Debug.Log(" Check reports " +
                      $"{sortedListOfLists.SafeContains(0, new TestClass("one"))}");

            Debug.Log($"Checking if a specific key-value (1,two) is contained using SafeContains");
            Debug.Log(" Check reports " +
                      $"{sortedListOfLists.SafeContains(1, new TestClass("two"))}");

            Debug.Log(
                $"Checking if a specific key-value (2,two) is contained using SafeContains. Note: the list is empty.");
            Debug.Log(" Check reports " +
                      $"{sortedListOfLists.SafeContains(2, new TestClass("two"))}");

            Debug.Log(
                $"Checking if a specific key-value (3,two) is contained using SafeContains. Note: the list is null.");
            Debug.Log($" Check reports {sortedListOfLists.SafeContains(3, new TestClass("two"))}");
            Debug.Log(
                "The previous check should NOT have thrown an error like Contains did, since there is now a null-check for the value-list in the SafeContains extension.");
        }

        private void TestAddOrAppend()
        {
            RestoreDataFromCache();
            Debug.Log("Testing AddOrAppend\n" +
                      "This extension adds a new key-value pair to a nested sorted list or append a value to an existing key’s list, " +
                      "ASSUMING the existing key’s list is NOT null. This extension NOT ensure that no duplicates are added to the key’s list.");
            PrintSortedList();

            //Add a key-value pair where the KEY DOES NOT EXIST in the sorted list, when the list is NOT null.
            Debug.Log(
                "Trying to add a key-value pair where the KEY DOES NOT EXIST in the sorted list, when the list is NOT null.");
            Debug.Log("Adding (4,one) to the sorted nested list using AddOrAppend.");
            sortedListOfLists.AddOrAppend(4, new TestClass("one"));
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the value IS NOT contained in a key's existing list, when the list is NOT null
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the value IS NOT contained in a key's existing list, when the list is NOT null.");
            Debug.Log("Adding (4,two) to the sorted nested list using AddOrAppend.");
            sortedListOfLists.AddOrAppend(4, new TestClass("two"));
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the value IS contained in a key's existing list, when the list is NOT null
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the value IS contained in a key's existing list, when the list is NOT null.");
            Debug.Log("Adding another (4,two) to the sorted nested list using AddOrAppend.");
            sortedListOfLists.AddOrAppend(4, new TestClass("two"));
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the list associated with that key IS NULL. 
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the list associated with that key IS NULL.");
            Debug.Log("Trying to add (3,two) to the sorted nested list using AddOrAppend.");
            string report = "No exception reported.";
            try
            {
                sortedListOfLists.AddOrAppend(3, new TestClass("two"));
            }
            catch (Exception e)
            {
                report = e.ToString();
            }

            Debug.Log(
                $"The report from AddOrAppend attempting to operate on a value-list that is null is the following: {report}");
            PrintSortedList();
        }

        private void TestSafeAddOrAppend()
        {
            RestoreDataFromCache();
            Debug.Log("Testing SafeAddOrAppend\n" +
                      "This extension adds a new key-value pair to a nested sorted list or append a value to an existing key’s list, " +
                      "NOT ASSUMING the existing key’s list is NOT null, but rather CHECKING FIRST if it is null, and ENSURING it is not before appending the value. " +
                      "This extension NOT ensure that no duplicates are added to the key’s list.");
            PrintSortedList();

            //Add a key-value pair where the KEY DOES NOT EXIST in the sorted list, when the list is NOT null.
            Debug.Log(
                "Trying to add a key-value pair where the KEY DOES NOT EXIST in the sorted list, when the list is NOT null.");
            Debug.Log("Adding (4,one) to the sorted nested list using SafeAddOrAppend.");
            sortedListOfLists.SafeAddOrAppend(4, new TestClass("one"));
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the value IS NOT contained in a key's existing list, when the list is NOT null
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the value IS NOT contained in a key's existing list, when the list is NOT null.");
            Debug.Log("Adding (4,two) to the sorted nested list using SafeAddOrAppend.");
            sortedListOfLists.SafeAddOrAppend(4, new TestClass("two"));
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the value IS contained in a key's existing list, when the list is NOT null
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the value IS contained in a key's existing list, when the list is NOT null.");
            Debug.Log("Adding another (4,two) to the sorted nested list using SafeAddOrAppend.");
            sortedListOfLists.SafeAddOrAppend(4, new TestClass("two"));
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the list associated with that key IS NULL. 
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the list associated with that key IS NULL.");
            Debug.Log("Trying to add (3,two) to the sorted nested list using SafeAddOrAppend.");
            string report = "No exception reported.";
            try
            {
                sortedListOfLists.SafeAddOrAppend(3, new TestClass("two"));
            }
            catch (Exception e)
            {
                report = e.ToString();
            }

            Debug.Log(
                $"The report from SafeAddOrAppend attempting to operate on a value-list that is null is the following: {report}");
            PrintSortedList();
        }

        private void TestAddOrAppendUniqueWithoutReplacing()
        {
            RestoreDataFromCache();
            Debug.Log("Testing AddOrAppendUnique without replacing/n" +
                      "This extension adds a new key-value pair to a nested sorted list or append a value to an existing key’s list, " +
                      "ASSUMING the existing key’s list is NOT null. " +
                      "This extension DOES ensure that no duplicates are added to the key’s list. " +
                      "This test operates by NOT REPLACING duplicate elements.");
            PrintSortedList();

            //Add a key-value pair where the KEY DOES NOT EXIST in the sorted list, when the list is NOT null.
            Debug.Log(
                "Trying to add a key-value pair where the KEY DOES NOT EXIST in the sorted list, when the list is NOT null.");
            Debug.Log("Adding (4,one) to the sorted nested list using AddOrAppendUnique without replacing.");
            sortedListOfLists.AddOrAppendUnique(4, new TestClass("one"));
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the value IS NOT contained in a key's existing list, when the list is NOT null
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the value IS NOT contained in a key's existing list, when the list is NOT null.");
            Debug.Log("Adding (4,two) to the sorted nested list using AddOrAppendUnique without replacing.");
            sortedListOfLists.AddOrAppendUnique(4, new TestClass("two"));
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the value IS contained in a key's existing list, when the list is NOT null
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the value IS contained in a key's existing list, when the list is NOT null.");
            Debug.Log(
                "Trying to add another (4,two) to the sorted nested list using AddOrAppendUnique without replacing.");
            int idOfOriginal = sortedListOfLists[4].Find(val => val.Equals(new TestClass("two"))).uniqueID;
            sortedListOfLists.AddOrAppendUnique(4, new TestClass("two", 1));
            string duplicateReport =
                (idOfOriginal == sortedListOfLists[4].Find(val => val.Equals(new TestClass("two"))).uniqueID)
                    ? "The added/appended duplicate has not replaced the original in the list."
                    : "The added/appended duplicate has replaced the original in the list.";
            Debug.Log(duplicateReport);
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the list associated with that key IS NULL. 
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the list associated with that key IS NULL.");
            Debug.Log("Trying to add (3,two) to the sorted nested list using AddOrAppendUnique without replacing.");
            string report = "No exception reported.";
            try
            {
                sortedListOfLists.AddOrAppendUnique(3, new TestClass("two"));
            }
            catch (Exception e)
            {
                report = e.ToString();
            }

            Debug.Log(
                $"The report from AddOrAppendUnique without replacing is attempting to operate on a value-list that is null is the following: {report}");
            PrintSortedList();
        }

        private void TestAddOrAppendUniqueWithReplacing()
        {
            RestoreDataFromCache();
            Debug.Log("Testing AddOrAppendUnique with replacing/n" +
                      "This extension adds a new key-value pair to a nested sorted list or append a value to an existing key’s list, " +
                      "ASSUMING the existing key’s list is NOT null. " +
                      "This extension DOES ensure that no duplicates are added to the key’s list. " +
                      "This test operates BY REPLACING duplicate elements.");
            PrintSortedList();

            //Add a key-value pair where the KEY DOES NOT EXIST in the sorted list, when the list is NOT null.
            Debug.Log(
                "Trying to add a key-value pair where the KEY DOES NOT EXIST in the sorted list, when the list is NOT null.");
            Debug.Log("Adding (4,one) to the sorted nested list using AddOrAppendUnique with replacing.");
            sortedListOfLists.AddOrAppendUnique(4, new TestClass("one"), true);
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the value IS NOT contained in a key's existing list, when the list is NOT null
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the value IS NOT contained in a key's existing list, when the list is NOT null.");
            Debug.Log("Adding (4,two) to the sorted nested list using AddOrAppendUnique with replacing.");
            sortedListOfLists.AddOrAppendUnique(4, new TestClass("two"), true);
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the value IS contained in a key's existing list, when the list is NOT null
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the value IS contained in a key's existing list, when the list is NOT null.");
            Debug.Log(
                "Trying to add another (4,two) to the sorted nested list using AddOrAppendUnique with replacing.");
            int idOfOriginal = sortedListOfLists[4].Find(val => val.Equals(new TestClass("two"))).uniqueID;
            sortedListOfLists.AddOrAppendUnique(4, new TestClass("two", 1), true);
            string duplicateReport =
                (idOfOriginal == sortedListOfLists[4].Find(val => val.Equals(new TestClass("two"))).uniqueID)
                    ? "The added/appended duplicate has not replaced the original in the list."
                    : "The added/appended duplicate has replaced the original in the list.";
            Debug.Log(duplicateReport);
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the list associated with that key IS NULL. 
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the list associated with that key IS NULL.");
            Debug.Log("Trying to add (3,two) to the sorted nested list using AddOrAppendUnique with replacing.");
            string report = "No exception reported.";
            try
            {
                sortedListOfLists.AddOrAppendUnique(3, new TestClass("two"), true);
            }
            catch (Exception e)
            {
                report = e.ToString();
            }

            Debug.Log(
                $"The report from AddOrAppendUnique attempting to operate on a value-list that is null is the following: {report}");
            PrintSortedList();
        }

        private void TestSafeAddOrAppendUniqueWithoutReplacing()
        {
            RestoreDataFromCache();
            Debug.Log("Testing SafeAddOrAppendUnique without replacing/n" +
                      "This extension adds a new key-value pair to a nested sorted list or append a value to an existing key’s list, " +
                      "NOT ASSUMING the existing key’s list is NOT null, but rather CHECKING FIRST if it is null, and ENSURING it is not before appending the value. " +
                      "This extension DOES ensure that no duplicates are added to the key’s list. " +
                      "This test operates by NOT REPLACING duplicate elements.");
            PrintSortedList();

            //Add a key-value pair where the KEY DOES NOT EXIST in the sorted list, when the list is NOT null.
            Debug.Log(
                "Trying to add a key-value pair where the KEY DOES NOT EXIST in the sorted list, when the list is NOT null.");
            Debug.Log("Adding (4,one) to the sorted nested list using SafeAddOrAppendUnique without replacing.");
            sortedListOfLists.SafeAddOrAppendUnique(4, new TestClass("one"));
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the value IS NOT contained in a key's existing list, when the list is NOT null
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the value IS NOT contained in a key's existing list, when the list is NOT null.");
            Debug.Log("Adding (4,two) to the sorted nested list using SafeAddOrAppendUnique without replacing.");
            sortedListOfLists.SafeAddOrAppendUnique(4, new TestClass("two"));
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the value IS contained in a key's existing list, when the list is NOT null
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the value IS contained in a key's existing list, when the list is NOT null.");
            Debug.Log(
                "Trying to add another (4,two) to the sorted nested list using SafeAddOrAppendUnique without replacing.");
            int idOfOriginal = sortedListOfLists[4].Find(val => val.Equals(new TestClass("two"))).uniqueID;
            sortedListOfLists.SafeAddOrAppendUnique(4, new TestClass("two", 1));
            string duplicateReport =
                (idOfOriginal == sortedListOfLists[4].Find(val => val.Equals(new TestClass("two"))).uniqueID)
                    ? "The added/appended duplicate has not replaced the original in the list."
                    : "The added/appended duplicate has replaced the original in the list.";
            Debug.Log(duplicateReport);
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the list associated with that key IS NULL. 
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the list associated with that key IS NULL.");
            Debug.Log("Trying to add (3,two) to the sorted nested list using SafeAddOrAppendUnique without replacing.");
            string report = "No exception reported.";
            try
            {
                sortedListOfLists.SafeAddOrAppendUnique(3, new TestClass("two"));
            }
            catch (Exception e)
            {
                report = e.ToString();
            }

            Debug.Log(
                $"The report from SafeAddOrAppendUnique attempting to operate on a value-list that is null is the following: {report}");
            PrintSortedList();
        }

        private void TestSafeAddOrAppendUniqueWithReplacing()
        {
            RestoreDataFromCache();
            Debug.Log("Testing SafeAddOrAppendUnique with replacing\n" +
                      "This extension adds a new key-value pair to a nested sorted list or append a value to an existing key’s list, " +
                      "NOT ASSUMING the existing key’s list is NOT null, but rather CHECKING FIRST if it is null, and ENSURING it is not before appending the value. " +
                      "This extension DOES ensure that no duplicates are added to the key’s list. " +
                      "This test operates by NOT REPLACING duplicate elements.");
            PrintSortedList();

            //Add a key-value pair where the KEY DOES NOT EXIST in the sorted list, when the list is NOT null.
            Debug.Log(
                "Trying to add a key-value pair where the KEY DOES NOT EXIST in the sorted list, when the list is NOT null.");
            Debug.Log("Adding (4,one) to the sorted nested list using SafeAddOrAppendUnique with replacing.");
            sortedListOfLists.SafeAddOrAppendUnique(4, new TestClass("one"), true);
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the value IS NOT contained in a key's existing list, when the list is NOT null
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the value IS NOT contained in a key's existing list, when the list is NOT null.");
            Debug.Log("Adding (4,two) to the sorted nested list using SafeAddOrAppendUnique with replacing.");
            sortedListOfLists.SafeAddOrAppendUnique(4, new TestClass("two"), true);
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the value IS contained in a key's existing list, when the list is NOT null
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the value IS contained in a key's existing list, when the list is NOT null.");
            Debug.Log(
                "Trying to add another (4,two) to the sorted nested list using SafeAddOrAppendUnique with replacing.");
            int idOfOriginal = sortedListOfLists[4].Find(val => val.Equals(new TestClass("two"))).uniqueID;
            sortedListOfLists.SafeAddOrAppendUnique(4, new TestClass("two", 1), true);
            string duplicateReport =
                (idOfOriginal == sortedListOfLists[4].Find(val => val.Equals(new TestClass("two"))).uniqueID)
                    ? "The added/appended duplicate has not replaced the original in the list."
                    : "The added/appended duplicate has replaced the original in the list.";
            Debug.Log(duplicateReport);
            PrintSortedList();

            //Add a key-value pair where the KEY EXISTS and the list associated with that key IS NULL. 
            Debug.Log(
                "Trying to add a key-value pair where the KEY EXISTS and the list associated with that key IS NULL.");
            Debug.Log("Trying to add (3,two) to the sorted nested list using SafeAddOrAppendUnique with replacing.");
            string report = "No exception reported.";
            try
            {
                sortedListOfLists.SafeAddOrAppendUnique(3, new TestClass("two"), true);
            }
            catch (Exception e)
            {
                report = e.ToString();
            }

            Debug.Log(
                $"The report from SafeAddOrAppendUnique attempting to operate on a value-list that is null is the following: {report}");
            PrintSortedList();
        }

        private void TestRemove()
        {
            RestoreDataFromCache();
            Debug.Log("Testing Remove\n" +
                      "This extension removes a key-value pair in the sorted list of lists. " +
                      "This extension DOES INDEED ASSUME that the key exists in the sorted list and the value is NOT null." +
                      "This extension DOES NOT delete the key in the sorted list if the value is an empty list. " +
                      "This extension DOES INDEED ASSUME the pair has been verified to exist prior to being called.");
            PrimeSortedList();
            PrintSortedList();

            bool successful;
            string errorReport;
            string noError = "No exception or error thrown.";

            //Try and remove a key value pair in which both the key and value exist
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS and value EXISTS for that key. The value's list will not be empty in the end, nor is it null.");
            Debug.Log("Remove (1,one) from the sorted nested list using Remove.");
            sortedListOfLists.Remove(1, new TestClass("one"));
            PrintSortedList();

            //Try and remove a key value pair in which the value is the only element in its list, leaving an empty list behind
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS and value EXISTS for that key in a NON-NULL list. HOWEVER, the value's list WILL BE EMPTY if the value is removed.");
            Debug.Log(
                "Remove (4,single) from the sorted nested list using Remove. The value's list should be empty and remain in the collection.");
            sortedListOfLists.Remove(4, new TestClass("single"));
            PrintSortedList();

            //Try and remove a key value pair in which the key exists but the value does not
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS but the value DOES NOT EXIST for that key in a NON-NULL list.");
            Debug.Log(
                "Remove (1,ghost) from the sorted nested list using Remove. This should simply leave the collection unchanged.");
            sortedListOfLists.Remove(1, new TestClass("ghost"));
            PrintSortedList();

            //Try and remove a key value pair in which the key does not exist
            Debug.Log(
                "Trying to remove a key-value pair in which the key DOES NOT EXIST.");
            Debug.Log(
                "Remove (5,one) from the sorted nested list using Remove. This should throw an error as Remove assumes the key exists.");

            errorReport = noError;
            try
            {
                sortedListOfLists.Remove(5, new TestClass("one"));
            }
            catch (Exception e)
            {
                errorReport = $"Error report: {e}";
            }

            Debug.Log(errorReport);
            PrintSortedList();

            //Try and remove a key value pair in which the key exists but the value is null
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS but the list associated with that key is NULL.");
            Debug.Log(
                "Remove (3,ghost) from the sorted nested list using Remove. This should throw an error as Remove assumes all lists are NOT NULL.");

            errorReport = noError;
            try
            {
                sortedListOfLists.Remove(3, new TestClass("ghost"));
                ;
            }
            catch (Exception e)
            {
                errorReport = $"Error report: {e}";
            }

            Debug.Log(errorReport);
            PrintSortedList();
            return;

            //Local function to prime sortedList for this test
            void PrimeSortedList()
            {
                sortedListOfLists.AddOrAppend(4, new TestClass("single"));
            }
        }

        private void TestThoroughRemove()
        {
            RestoreDataFromCache();
            Debug.Log("Testing ThoroughRemove\n" +
                      "This extension removes a key-value pair in the sorted list of lists. " +
                      "This extension DOES INDEED ASSUME that the key exists in the sorted list and the value is NOT null." +
                      "This extension DOES INDEED delete the key in the sorted list if the value is an empty list. " +
                      "This extension DOES INDEED ASSUME the pair has been verified to exist prior to being called.");
            PrimeSortedList();
            PrintSortedList();

            bool successful;
            string errorReport;
            string noError = "No exception or error thrown.";

            //Try and remove a key value pair in which both the key and value exist
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS and value EXISTS for that key. The value's list will not be empty in the end, nor is it null.");
            Debug.Log("Remove (1,one) from the sorted nested list using ThoroughRemove.");
            sortedListOfLists.ThoroughRemove(1, new TestClass("one"));
            PrintSortedList();

            //Try and remove a key value pair in which the value is the only element in its list, leaving an empty list behind
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS and value EXISTS for that key in a NON-NULL list. HOWEVER, the value's list WILL BE EMPTY if the value is removed.");
            Debug.Log(
                "Remove (4,single) from the sorted nested list using ThoroughRemove. The value's list should be empty, and the key-value pair should be entirely removed from the collection.");
            sortedListOfLists.ThoroughRemove(4, new TestClass("single"));
            PrintSortedList();

            //Try and remove a key value pair in which the key exists but the value does not
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS but the value DOES NOT EXIST for that key in a NON-NULL list.");
            Debug.Log(
                "Remove (1,ghost) from the sorted nested list using ThoroughRemove. This should simply leave the collection unchanged.");
            sortedListOfLists.ThoroughRemove(1, new TestClass("ghost"));
            PrintSortedList();

            //Try and remove a key value pair in which the key does not exist
            Debug.Log(
                "Trying to remove a key-value pair in which the key DOES NOT EXIST.");
            Debug.Log(
                "Remove (5,one) from the sorted nested list using ThoroughRemove. This should throw an error as ThoroughRemove assumes the key exists.");

            errorReport = noError;
            try
            {
                sortedListOfLists.ThoroughRemove(5, new TestClass("one"));
            }
            catch (Exception e)
            {
                errorReport = $"Error report: {e}";
            }

            Debug.Log(errorReport);
            PrintSortedList();

            //Try and remove a key value pair in which the key exists but the value is null
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS but the list associated with that key is NULL.");
            Debug.Log(
                "Remove (3,ghost) from the sorted nested list using ThoroughRemove. This should throw an error as ThoroughRemove assumes all lists are NOT NULL.");

            errorReport = noError;
            try
            {
                sortedListOfLists.ThoroughRemove(3, new TestClass("ghost"));
                ;
            }
            catch (Exception e)
            {
                errorReport = $"Error report: {e}";
            }

            Debug.Log(errorReport);
            PrintSortedList();
            return;

            //Local function to prime sortedList for this test
            void PrimeSortedList()
            {
                sortedListOfLists.AddOrAppend(4, new TestClass("single"));
            }
        }

        private void TestTryRemove()
        {
            RestoreDataFromCache();
            Debug.Log("Testing TryRemove\n" +
                      "This extension TRIES to remove a key-value pair in the sorted list of lists. " +
                      "This extension DOES NOT ASSUME that the key exists in the sorted list prior to being called. " +
                      "This extension DOES NOT ASSUME that the value exists for that key in the sorted list prior to being called. " +
                      "This extension DOES INDEED ASSUME that the value is NOT null." +
                      "This extension DOES NOT delete the key in the sorted list if the value is an empty list. ");
            PrimeSortedList();
            PrintSortedList();

            bool successful;
            string errorReport;
            string noError = "No exception or error thrown.";

            //Try and remove a key value pair in which both the key and value exist
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS and value EXISTS for that key. The value's list will not be empty in the end, nor is it null.");
            successful = sortedListOfLists.TryRemove(1, new TestClass("one"));
            Debug.Log(
                $"Removing (1,one) from the sorted nested list using TryRemove reports a success boolean of {successful}");
            PrintSortedList();

            //Try and remove a key value pair in which the value is the only element in its list, leaving an empty list behind
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS and value EXISTS for that key in a NON-NULL list. HOWEVER, the value's list WILL BE EMPTY if the value is removed.");
            successful = sortedListOfLists.TryRemove(4, new TestClass("single"));
            Debug.Log(
                $"Remove (4,single) from the sorted nested list using TryRemove  reports a success boolean of {successful}. " +
                $"The value's list should be empty and remain in the collection.");
            PrintSortedList();

            //Try and remove a key value pair in which the key exists but the value does not
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS but the value DOES NOT EXIST for that key in a NON-NULL list.");
            successful = sortedListOfLists.TryRemove(1, new TestClass("ghost"));
            Debug.Log(
                $"Remove (1,ghost) from the sorted nested list using TryRemove reports a success boolean of {successful}. " +
                $"This should simply leave the collection unchanged.");
            PrintSortedList();

            //Try and remove a key value pair in which the key does not exist
            Debug.Log(
                "Trying to remove a key-value pair in which the key DOES NOT EXIST.");
            successful = sortedListOfLists.TryRemove(5, new TestClass("one"));
            Debug.Log(
                $"Remove (5,one) from the sorted nested list using TryRemove reports a success boolean of {successful}. " +
                "Unlike Remove, TryRemove checks if the key exists.");
            PrintSortedList();

            //Try and remove a key value pair in which the key exists but the value is null
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS but the list associated with that key is NULL.");
            Debug.Log(
                "Remove (3,ghost) from the sorted nested list using TryRemove. This should throw an error as TryRemove assumes all lists are NOT NULL.");

            errorReport = noError;
            try
            {
                sortedListOfLists.TryRemove(3, new TestClass("ghost"));
                ;
            }
            catch (Exception e)
            {
                errorReport = $"Error report: {e}";
            }

            Debug.Log(errorReport);
            PrintSortedList();
            return;

            //Local function to prime sortedList for this test
            void PrimeSortedList()
            {
                sortedListOfLists.AddOrAppend(4, new TestClass("single"));
            }
        }

        private void TestTryThoroughRemove()
        {
            RestoreDataFromCache();
            Debug.Log("Testing TryThoroughRemove\n" +
                      "This extension TRIES to remove a key-value pair in the sorted list of lists. " +
                      "This extension DOES NOT ASSUME that the key exists in the sorted list prior to being called. " +
                      "This extension DOES NOT ASSUME that the value exists for that key in the sorted list prior to being called. " +
                      "This extension DOES INDEED ASSUME that the value is NOT null." +
                      "This extension DOES INDEED delete the key in the sorted list if the value is an empty list. ");
            PrimeSortedList();
            PrintSortedList();

            bool successful;
            string errorReport;
            string noError = "No exception or error thrown.";

            //Try and remove a key value pair in which both the key and value exist
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS and value EXISTS for that key. The value's list will not be empty in the end, nor is it null.");
            successful = sortedListOfLists.TryThoroughRemove(1, new TestClass("one"));
            Debug.Log(
                $"Removing (1,one) from the sorted nested list using TryThoroughRemove reports a success boolean of {successful}");
            PrintSortedList();

            //Try and remove a key value pair in which the value is the only element in its list, leaving an empty list behind
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS and value EXISTS for that key in a NON-NULL list. HOWEVER, the value's list WILL BE EMPTY if the value is removed.");
            successful = sortedListOfLists.TryThoroughRemove(4, new TestClass("single"));
            Debug.Log(
                $"Remove (4,single) from the sorted nested list using TryThoroughRemove  reports a success boolean of {successful}. " +
                $"The value's list should end up empty, and therefore the key-value pair should be entirely removed from the collection.");
            PrintSortedList();

            //Try and remove a key value pair in which the key exists but the value does not
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS but the value DOES NOT EXIST for that key in a NON-NULL list.");
            successful = sortedListOfLists.TryThoroughRemove(1, new TestClass("ghost"));
            Debug.Log(
                $"Remove (1,ghost) from the sorted nested list using TryThoroughRemove reports a success boolean of {successful}. " +
                $"This should simply leave the collection unchanged.");
            PrintSortedList();

            //Try and remove a key value pair in which the key does not exist
            Debug.Log(
                "Trying to remove a key-value pair in which the key DOES NOT EXIST.");
            successful = sortedListOfLists.TryThoroughRemove(5, new TestClass("one"));
            Debug.Log(
                $"Remove (5,one) from the sorted nested list using TryThoroughRemove reports a success boolean of {successful}. " +
                "Unlike ThoroughRemove, TryThoroughRemove checks if the key exists.");
            PrintSortedList();

            //Try and remove a key value pair in which the key exists but the value is null
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS but the list associated with that key is NULL.");
            Debug.Log(
                "Remove (3,ghost) from the sorted nested list using TryThoroughRemove. This should throw an error as TryThoroughRemove assumes all lists are NOT NULL.");

            errorReport = noError;
            try
            {
                sortedListOfLists.TryThoroughRemove(3, new TestClass("ghost"));
                
            }
            catch (Exception e)
            {
                errorReport = $"Error report: {e}";
            }

            Debug.Log(errorReport);
            PrintSortedList();
            return;

            //Local function to prime sortedList for this test
            void PrimeSortedList()
            {
                sortedListOfLists.AddOrAppend(4, new TestClass("single"));
            }
        }

        private void TestTrySafeRemove()
        {
            RestoreDataFromCache();
            Debug.Log("Testing TrySafeRemove\n" +
                      "This extension TRIES to remove a key-value pair in the sorted list of lists. " +
                      "This extension DOES NOT ASSUME that the key exists in the sorted list prior to being called. " +
                      "This extension DOES NOT ASSUME that the value exists for that key in the sorted list prior to being called. " +
                      "This extension DOES NOT ASSUME that the value is NOT null." +
                      "This extension DOES NOT delete the key in the sorted list if the value is an empty list. ");
                        PrimeSortedList();
            PrintSortedList();

            bool successful;
            string errorReport;
            string noError = "No exception or error thrown.";

            //Try and remove a key value pair in which both the key and value exist
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS and value EXISTS for that key. The value's list will not be empty in the end, nor is it null.");
            successful = sortedListOfLists.TrySafeRemove(1, new TestClass("one"));
            Debug.Log(
                $"Removing (1,one) from the sorted nested list using TrySafeRemove reports a success boolean of {successful}");
            PrintSortedList();

            //Try and remove a key value pair in which the value is the only element in its list, leaving an empty list behind
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS and value EXISTS for that key in a NON-NULL list. HOWEVER, the value's list WILL BE EMPTY if the value is removed.");
            successful = sortedListOfLists.TrySafeRemove(4, new TestClass("single"));
            Debug.Log(
                $"Remove (4,single) from the sorted nested list using TrySafeRemove  reports a success boolean of {successful}. " +
                $"The value's list should be empty and remain in the collection.");
            PrintSortedList();

            //Try and remove a key value pair in which the key exists but the value does not
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS but the value DOES NOT EXIST for that key in a NON-NULL list.");
            successful = sortedListOfLists.TrySafeRemove(1, new TestClass("ghost"));
            Debug.Log(
                $"Remove (1,ghost) from the sorted nested list using TrySafeRemove reports a success boolean of {successful}. " +
                $"This should simply leave the collection unchanged.");
            PrintSortedList();

            //Try and remove a key value pair in which the key does not exist
            Debug.Log(
                "Trying to remove a key-value pair in which the key DOES NOT EXIST.");
            successful = sortedListOfLists.TrySafeRemove(5, new TestClass("one"));
            Debug.Log(
                $"Remove (5,one) from the sorted nested list using TrySafeRemove reports a success boolean of {successful}. " +
                "Unlike Remove, TrySafeRemove checks if the key exists.");
            PrintSortedList();

            //Try and remove a key value pair in which the key exists but the value is null
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS but the list associated with that key is NULL.");
            successful = sortedListOfLists.TrySafeRemove(3, new TestClass("ghost"));
            Debug.Log(
                $"Remove (3,ghost) from the sorted nested list using TrySafeRemove reports a success boolean of {successful}. " +
                "This should throw NOT throw an error as TrySafeRemove checks for null lists.");
            PrintSortedList();
            return;

            //Local function to prime sortedList for this test
            void PrimeSortedList()
            {
                sortedListOfLists.AddOrAppend(4, new TestClass("single"));
            }
        }

        private void TestTryThoroughSafeRemove()
        {
            RestoreDataFromCache();
            Debug.Log("Testing TryThoroughSafeRemove\n" +
                      "This extension TRIES to remove a key-value pair in the sorted list of lists. " +
                      "This extension DOES NOT ASSUME that the key exists in the sorted list prior to being called. " +
                      "This extension DOES NOT ASSUME that the value exists for that key in the sorted list prior to being called. " +
                      "This extension DOES NOT ASSUME that the value is NOT null." +
                      "This extension DOES INDEED delete the key in the sorted list if the value is an empty list. ");
            PrimeSortedList();
            PrintSortedList();

            bool successful;
            string errorReport;
            string noError = "No exception or error thrown.";

            //Try and remove a key value pair in which both the key and value exist
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS and value EXISTS for that key. The value's list will not be empty in the end, nor is it null.");
            successful = sortedListOfLists.TryThoroughSafeRemove(1, new TestClass("one"));
            Debug.Log(
                $"Removing (1,one) from the sorted nested list using TryThoroughSafeRemove reports a success boolean of {successful}");
            PrintSortedList();

            //Try and remove a key value pair in which the value is the only element in its list, leaving an empty list behind
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS and value EXISTS for that key in a NON-NULL list. HOWEVER, the value's list WILL BE EMPTY if the value is removed.");
            successful = sortedListOfLists.TryThoroughSafeRemove(4, new TestClass("single"));
            Debug.Log(
                $"Remove (4,single) from the sorted nested list using TryThoroughSafeRemove  reports a success boolean of {successful}. " +
                $"The value's list should end up empty, and therefore the key-value pair should be entirely removed from the collection.");
            PrintSortedList();

            //Try and remove a key value pair in which the key exists but the value does not
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS but the value DOES NOT EXIST for that key in a NON-NULL list.");
            successful = sortedListOfLists.TryThoroughSafeRemove(1, new TestClass("ghost"));
            Debug.Log(
                $"Remove (1,ghost) from the sorted nested list using TryThoroughSafeRemove reports a success boolean of {successful}. " +
                $"This should simply leave the collection unchanged.");
            PrintSortedList();

            //Try and remove a key value pair in which the key does not exist
            Debug.Log(
                "Trying to remove a key-value pair in which the key DOES NOT EXIST.");
            successful = sortedListOfLists.TryThoroughSafeRemove(5, new TestClass("one"));
            Debug.Log(
                $"Remove (5,one) from the sorted nested list using TryThoroughSafeRemove reports a success boolean of {successful}. " +
                "Unlike ThoroughRemove, TryThoroughSafeRemove checks if the key exists.");
            PrintSortedList();

            //Try and remove a key value pair in which the key exists but the value is null
            Debug.Log(
                "Trying to remove a key-value pair in which the key EXISTS but the list associated with that key is NULL.");
            successful = sortedListOfLists.TryThoroughSafeRemove(3, new TestClass("ghost"));
            Debug.Log(
                $"Remove (3,ghost) from the sorted nested list using TryThoroughSafeRemove reports a success boolean of {successful}. " +
                "This should throw NOT throw an error as TrySafeRemove checks for null lists.");
            PrintSortedList();
            return;

            //Local function to prime sortedList for this test
            void PrimeSortedList()
            {
                sortedListOfLists.AddOrAppend(4, new TestClass("single"));
            }
        }
        
        private void TestFormat()
        {
            Debug.Log("Testing Format\n" +
                      "This extension formats a sorted list of lists according to the passed in specifications of the user.\n " +
                      "The user can request that null value-lists be removed and/or empty value-lists be removed. ");
            PrimeSortedList();
            PrintSortedList();
            
            //Try and remove all null lists, but keep the empty ones
            Debug.Log(
                "Trying to format a sorted list by removing all null lists, but keeping empty ones.");
            sortedListOfLists.Format(true, false);
            Debug.Log(
                $"Removing all null lists, and keeping empty ones from values in sorted list.");
            PrintSortedList();
            
            //Try and remove all empty lists, but keep the null ones
            PrimeSortedList();
            PrintSortedList();
            Debug.Log(
                "Trying to format a sorted list by removing all empty lists, but keeping null ones.");
            sortedListOfLists.Format(false, true);
            Debug.Log(
                $"Removing all empty lists, and keeping null ones from values in sorted list.");
            PrintSortedList();
            
            //Try and remove all null or empty lists
            PrimeSortedList();
            PrintSortedList();
            Debug.Log(
                "Trying to format a sorted list by removing all null or empty lists.");
            sortedListOfLists.Format(true, true);
            Debug.Log(
                $"Removing all null or empty lists in the sorted list.");
            PrintSortedList();
            
            void PrimeSortedList()
            {
                InitializeData();
                sortedListOfLists.Add(4, new List<TestClass>());
                sortedListOfLists.Add(5, null);
            }
        }

        #region Custom Tests

        // Helper method to print the sorted list
        void SimplePrintSortedList()
        {
            foreach (var pair in sortedListOfLists)
            {
                Debug.Log(
                    $"Key: {pair.Key}, Values: [{string.Join(", ", pair.Value.Select(v => (v != null) ? v.name.ToString() : "null"))}] - Count: {pair.Value.Count}");
            }
        }

        private void CustomTest()
        {
            // Initialize sortedListOfLists somewhere before this code
            //Get rid of null for just this test
            sortedListOfLists[3] = new List<TestClass>();

            Debug.Log("Testing AddOrAppendUnique with replacing\n" +
                      "This extension adds a new key-value pair to a nested sorted list or appends a value to an existing key’s list, " +
                      "ASSUMING the existing key’s list is NOT null. " +
                      "This extension DOES ensure that no duplicates are added to the key’s list.");

            // Test with replace = true
            SimplePrintSortedList();

            Debug.Log("Adding (4, one) to the sorted nested list using AddOrAppendUnique with replacing.");
            sortedListOfLists.AddOrAppendUnique(4, new TestClass("one"), true);
            SimplePrintSortedList();

            Debug.Log("Adding (4, two) to the sorted nested list using AddOrAppendUnique with replacing.");
            sortedListOfLists.AddOrAppendUnique(4, new TestClass("two"), true);
            SimplePrintSortedList();

            Debug.Log("Adding another (4, two) to the sorted nested list using AddOrAppendUnique with replacing.");
            int idOfOriginal = sortedListOfLists[4].Find(val => val.Equals(new TestClass("two"))).uniqueID;
            sortedListOfLists.AddOrAppendUnique(4, new TestClass("two", 1), true);

            // Check if the duplicate has been replaced correctly
            string duplicateReport =
                (idOfOriginal == sortedListOfLists[4].Find(val => val.Equals(new TestClass("two"))).uniqueID)
                    ? "The added/appended duplicate has not replaced the original in the list."
                    : "The added/appended duplicate has replaced the original in the list.";
            Debug.Log(duplicateReport);

            SimplePrintSortedList();

            // Test with replace = false
            Debug.Log("Testing AddOrAppendUnique without replacing\n" +
                      "This extension adds a new key-value pair to a nested sorted list or appends a value to an existing key’s list, " +
                      "ASSUMING the existing key’s list is NOT null. " +
                      "This extension DOES ensure that no duplicates are added to the key’s list. " +
                      "This test operates by NOT REPLACING duplicate elements.");

            SimplePrintSortedList();

            Debug.Log("Adding (5, one) to the sorted nested list using AddOrAppendUnique without replacing.");
            sortedListOfLists.AddOrAppendUnique(5, new TestClass("one"));
            SimplePrintSortedList();

            Debug.Log("Adding (5, two) to the sorted nested list using AddOrAppendUnique without replacing.");
            sortedListOfLists.AddOrAppendUnique(5, new TestClass("two"));
            SimplePrintSortedList();

            Debug.Log("Adding another (5, two) to the sorted nested list using AddOrAppendUnique without replacing.");
            idOfOriginal = sortedListOfLists[5].Find(val => val.Equals(new TestClass("two"))).uniqueID;
            sortedListOfLists.AddOrAppendUnique(5, new TestClass("two", 1));

            // Check if the duplicate has not been replaced and list size remains unchanged
            duplicateReport =
                (idOfOriginal == sortedListOfLists[5].Find(val => val.Equals(new TestClass("two"))).uniqueID)
                    ? "The added/appended duplicate has not replaced the original in the list."
                    : "The added/appended duplicate has replaced the original in the list.";
            Debug.Log(duplicateReport);

            SimplePrintSortedList();
            InitializeData();
            PrintSortedList();
        }

        #endregion

        #endregion

        #region Helper Methods

        private void PrintSortedList() => PrintSortedList(sortedListOfLists,
            testClass => (testClass.name == "") ? "empty string" : testClass.name);

        private void PrintSortedList<TKey, TValue>(SortedList<TKey, List<TValue>> sortedList,
            Func<TValue, string> convertToString)
        {
            string printedList = $"Printing sorted list, {sortedList}:\n";
            var keys = sortedList.Keys;
            for (int keyIndex = 0; keyIndex < sortedList.Count; keyIndex++)
            {
                var key = keys[keyIndex];
                printedList += $"| {key}, {PrintList(sortedList[key], convertToString)} |\n";
            }

            Debug.Log(printedList);
        }

        private string PrintList(List<TestClass> list) => PrintList(list,
            testClass => (testClass.name == "") ? "empty string" : testClass.name);

        private string PrintList<TValue>(List<TValue> list, Func<TValue, string> convertToString)
        {
            if (list == null)
            {
                return "null";
            }

            if (list.Count == 0)
            {
                return "empty";
            }

            string printList = "( ";
            for (int i = 0; i < list.Count; i++)
            {
                printList += $"{convertToString(list[i])}";
                if (i != list.Count - 1)
                {
                    printList += ", ";
                }
            }

            printList += $" ) - count: {list.Count} ";

            return printList;
        }

        #endregion

        #region Simulation Reporting

        protected override void ReportSimulationStart() => Debug.Log("Simulation has started.");

        protected override void ReportSimulationReset()
        {
            Debug.Log("Resetting Simulation: Your starting sorted list is the following:\n");
            PrintSortedList();
        }

        protected override void ReportSimulationEnd()
        {
            Debug.Log("Simulation has ended.");
        }

        #endregion

        #region Data Handling

        protected override void StoreCachedData()
        {
            InitializeData(ref cachedSortedListOfLists);
        }

        protected override void RestoreDataFromCache()
        {
            InitializeData();
            PrintSortedList();
        }

        protected override void InitializeData()
        {
            InitializeData(ref sortedListOfLists);
        }

        protected virtual void InitializeData(ref SortedList<int, List<TestClass>> nestedSortedList)
        {
            nestedSortedList = new SortedList<int, List<TestClass>>()
            {
                {
                    0, new List<TestClass>()
                    {
                        new TestClass("one"),
                        new TestClass("two"),
                        new TestClass("three")
                    }
                },
                {
                    1, new List<TestClass>()
                    {
                        new TestClass("one"),
                        new TestClass("one"),
                        new TestClass("two")
                    }
                },
                { 2, new List<TestClass>() },
                { 3, null }
            };
        }

        #endregion

        protected class TestClass : IEquatable<TestClass> //, IComparer<TestClass>
        {
            public string name;
            public int uniqueID = 0;
            public TestClass(string name) => this.name = name;
            public TestClass(string name, int id) : this(name) => this.uniqueID = id;

            public TestClass DeepCopy() => new TestClass(name, uniqueID);

            public bool Equals(TestClass other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return name == other.name;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((TestClass)obj);
            }

            public override int GetHashCode()
            {
                return (name != null ? name.GetHashCode() : 0);
            }

            /*private sealed class NameEqualityComparer : IEqualityComparer<TestClass>
            {
                public bool Equals(TestClass x, TestClass y)
                {
                    if (ReferenceEquals(x, y)) return true;
                    if (ReferenceEquals(x, null)) return false;
                    if (ReferenceEquals(y, null)) return false;
                    if (x.GetType() != y.GetType()) return false;
                    return x.name == y.name;
                }

                public int GetHashCode(TestClass obj)
                {
                    return (obj.name != null ? obj.name.GetHashCode() : 0);
                }
            }

            public static IEqualityComparer<TestClass> NameComparer { get; } = new NameEqualityComparer();

            public int Compare(TestClass x, TestClass y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;
                return string.Compare(x.name, y.name, StringComparison.Ordinal);
            }*/
        }
    }
}