﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nursery_Management_System_WPF
{
    /// <summary>
    /// Interaction logic for RowTemplate.xaml
    /// </summary>
    public partial class RowTemplate : UserControl
    {
        //I wanna send a child , parent or staff to the form 

        //set 0 for child , 1 for parnet , 2 for staff
        int idx = -1, cIdx = 0 , pIdx = 0 , sIdx = 0;
        Child mChild;
        Parent mParent;
        Staff mStaff;
        LinkedList<Child> child;
        LinkedList<Parent> parent;
        LinkedList<Staff> staff;
        adminWindow aWindow;
        parentWindow pWindow;
        staffWindow sWindow;    
        Grid super;
        
        //specify previous form
        //0 coming from parent , 1 coming from staff , 2 coming from admin 
        int previousForm = -1;

        public RowTemplate(int idx , int previousForm  , int cIdx , int pIdx , int sIdx , LinkedList<Child> child , LinkedList<Parent> parent , LinkedList<Staff> staff, Grid super , adminWindow aWindow , parentWindow pWindow , staffWindow sWindow)
        {
            InitializeComponent();
            this.idx = idx;
            this.child = child;
            this.parent = parent;
            this.staff = staff;
            this.previousForm = previousForm;
            this.super = super;
            this.aWindow = aWindow;
            this.pWindow = pWindow;
            this.sWindow = sWindow;
            this.cIdx = cIdx;
            this.pIdx = pIdx;
            this.sIdx = sIdx;

            initialize();
        }

        void initialize()
        {
            if(previousForm == 0 || previousForm == 1)
            {
                acceptButton.Visibility = Visibility.Hidden;
                declineButton.Visibility = Visibility.Hidden;
            }

            if(idx == 0)
            {
                name.Content = child.ElementAt(cIdx).firstName + " " + child.ElementAt(cIdx).lastName;
                mChild = child.ElementAt(cIdx);
            }
            else if(idx == 1)
            {
                name.Content = parent.ElementAt(pIdx).firstName + " " + parent.ElementAt(pIdx).lastName;
                mParent = parent.ElementAt(pIdx);
            }
            else if(idx == 2)
            {
                name.Content = staff.ElementAt(sIdx).firstName + " " + staff.ElementAt(sIdx).lastName;
                mStaff = staff.ElementAt(sIdx);
            }

            if (previousForm == 2)
            {
                acceptButton.Visibility = Visibility.Visible;
                declineButton.Visibility = Visibility.Visible;
            }
        }

        private void parentGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(previousForm == 0)
            {
                childWindow cw = new childWindow(pWindow);
                cw.dailyDetails.IsReadOnly = true;
                GlobalVariables.globalChild = mChild;
                cw.sendFeedback.Visibility = Visibility.Hidden;
                cw.roomID.IsEnabled = false;
                cw.editProfileButton.Visibility = Visibility.Visible;
                cw.showFeedback.Visibility = Visibility.Visible;
                cw.fillProfile();
                cw.ShowDialog();
            }
            else if(previousForm == 1)
            {
                childWindow cw = new childWindow(pWindow);
                cw.roomID.IsReadOnly = true;
                cw.childName.IsReadOnly = true;
                cw.DOBpicker.IsEnabled = false;
                cw.editProfileButton.Visibility = Visibility.Hidden;
                cw.female.IsEnabled = false;
                cw.male.IsEnabled = false;
                cw.showFeedback.Visibility = Visibility.Hidden;
                GlobalVariables.globalChild = mChild;
                cw.fillProfile();
                cw.ShowDialog();
            }
            else if(previousForm == 2)
            {
                if(idx == 0) // Holding child
                {
                    GlobalVariables.globalChild = mChild;
                    childSignUp window = new childSignUp();
                    window.signUpButton.Visibility = Visibility.Hidden;
                    
                    
                    //fill child data here
                    window.fillCdata();
                    window.disabledChild_info();
                    
                    window.ShowDialog();
                }
                else if(idx == 1) // Holding Parent
                {
                    GlobalVariables.globalParent = mParent;
                    parentSignUp window = new parentSignUp();
                    //fill parent data here
                    window.fillPdata1();
                    window.disabledParent_info1();
                    
                    //fill parent data here

                    window.ShowDialog();
                }
                else if(idx == 2) //Holding Staff
                {
                    GlobalVariables.globalStaff = mStaff;
                    staffSignUp window = new staffSignUp();
                    window.signUpButton.Visibility = Visibility.Hidden;
                    //fill staff data here
                    window.fillSdata();
                    window.disabledStaff();
                    
                    //fill staff data here

                    window.ShowDialog();
                }
            }
            else if(previousForm == 3)
            {
                //disable all texts and buttons

                if (idx == 0) // Holding child
                {
                    childSignUp window = new childSignUp();
                    //fill child data here

                    window.ShowDialog();
                }
                else if (idx == 1) // Holding Parent
                {
                    parentSignUp window = new parentSignUp();

                    //fill parent data here

                    window.ShowDialog();
                }
                else if (idx == 2) //Holding Staff
                {
                    staffSignUp window = new staffSignUp();

                    //fill staff data here

                    window.ShowDialog();
                }
            }
        }

        public void removeFromParent()
        {
            /*if(previousForm == 0)
            {
                if (idx == 0)
                {
                    if (aWindow.childRow.Count != 0)
                    {
                        if (child.Count != 1)
                        {
                            cIdx = child.Count - 1;
                            initialize();
                        }
                        super.Children.Remove(aWindow.childRow.ElementAt(aWindow.childRow.Count - 1));
                        aWindow.childRow.RemoveLast();
                        child.Remove(child.ElementAt(child.Count - 1));

                    }
                }
                else if (idx == 1)
                {
                    if (aWindow.parentRow.Count != 0)
                    {
                        if (parent.Count != 1)
                        {
                            pIdx = parent.Count - 1;
                            initialize();
                        }
                        super.Children.Remove(aWindow.parentRow.ElementAt(aWindow.parentRow.Count - 1));
                        aWindow.parentRow.RemoveLast();
                        parent.Remove(parent.ElementAt(parent.Count - 1));

                    }
                }
                else if (idx == 2)
                {
                    if (aWindow.staffRow.Count != 0)
                    {
                        if (staff.Count != 1)
                        {
                            sIdx = staff.Count - 1;
                            initialize();
                        }
                        super.Children.Remove(aWindow.staffRow.ElementAt(aWindow.staffRow.Count - 1));
                        aWindow.staffRow.RemoveLast();
                        staff.Remove(staff.ElementAt(staff.Count - 1));

                    }
                }
            }
            else if(previousForm == 1)
            { 
                if(idx == 0)
                {
                    if(aWindow.childRow.Count != 0)
                    {
                        if(child.Count != 1)
                        {
                            cIdx = child.Count - 1;
                            initialize();
                        }
                        super.Children.Remove(aWindow.childRow.ElementAt(aWindow.childRow.Count - 1));
                        aWindow.childRow.RemoveLast();
                        child.Remove(child.ElementAt(child.Count - 1));

                    }
                }
                else if(idx == 1)
                {
                    if(aWindow.parentRow.Count != 0)
                    {
                        if (parent.Count != 1)
                        {
                            pIdx = parent.Count - 1;
                            initialize();
                        }
                        super.Children.Remove(aWindow.parentRow.ElementAt(aWindow.parentRow.Count - 1));
                        aWindow.parentRow.RemoveLast();
                        parent.Remove(parent.ElementAt(parent.Count - 1));

                    }
                }
                else if(idx == 2)
                {
                    if(aWindow.staffRow.Count != 0)
                    {
                        if (staff.Count != 1)
                        {
                            sIdx = staff.Count - 1;
                            initialize();
                        }
                        super.Children.Remove(aWindow.staffRow.ElementAt(aWindow.staffRow.Count - 1));
                        aWindow.staffRow.RemoveLast();
                        staff.Remove(staff.ElementAt(staff.Count - 1));

                    }
                } 
            }
            else*/ if (previousForm == 2)
            {
                if(idx == 0)
                {
                    if(aWindow.childRow.Count != 0)
                    {
                        if(child.Count != 1)
                        {
                            cIdx = child.Count - 1;
                            initialize();
                        }
                        super.Children.Remove(aWindow.childRow.ElementAt(aWindow.childRow.Count - 1));
                        aWindow.childRow.RemoveLast();
                        child.Remove(child.ElementAt(child.Count - 1));

                    }
                }
                else if(idx == 1)
                {
                    if(aWindow.parentRow.Count != 0)
                    {
                        if (parent.Count != 1)
                        {
                            pIdx = parent.Count - 1;
                            initialize();
                        }
                        super.Children.Remove(aWindow.parentRow.ElementAt(aWindow.parentRow.Count - 1));
                        aWindow.parentRow.RemoveLast();
                        parent.Remove(parent.ElementAt(parent.Count - 1));

                    }
                }
                else if(idx == 2)
                {
                    if(aWindow.staffRow.Count != 0)
                    {
                        if (staff.Count != 1)
                        {
                            sIdx = staff.Count - 1;
                            initialize();
                        }
                        super.Children.Remove(aWindow.staffRow.ElementAt(aWindow.staffRow.Count - 1));
                        aWindow.staffRow.RemoveLast();
                        staff.Remove(staff.ElementAt(staff.Count - 1));

                    }
                }
            }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            //set pending to 0 ---> accepted
            SQLQuery mSQLQuery = new SQLQuery();
            if (idx == 0)
            {
                mChild.pending = 0;
                mSQLQuery.updateChildData(mChild);
            }
            else if(idx == 1)
            {
                mParent.pending = 0;
                mSQLQuery.updateParentData(mParent);
            }
            else if(idx == 2)
            {
                mStaff.pending = 0;
                mSQLQuery.updateStaffData(mStaff);
            }

            removeFromParent();
        }
        
        private void declineButton_Click(object sender, RoutedEventArgs e)
        {
            //rejecting request
            SQLQuery mSQLQuery = new SQLQuery();
            if (idx == 0)
            {
                LinkedList<int> toDel = new LinkedList<int>();
                toDel.AddLast((int)child.ElementAt(cIdx).id);
                mSQLQuery.deleteChildData(toDel);
            }
            else if (idx == 1)
            {
                LinkedList<Int64> toDel = new LinkedList<Int64>();
                toDel.AddLast(parent.ElementAt(pIdx).id);
                mSQLQuery.deleteParentData(toDel);
            }
            else if (idx == 2)
            {
                LinkedList<Int64> toDel = new LinkedList<Int64>();
                toDel.AddLast(staff.ElementAt(sIdx).id);
                mSQLQuery.deleteStaffData(toDel);
            }

            removeFromParent();
        }     
    }
}
