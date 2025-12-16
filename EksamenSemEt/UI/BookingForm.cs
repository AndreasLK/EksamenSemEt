using DatabaseAccessSem1.Repository;
using EksamenSemEt.DatabaseAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EksamenSemEt.UI
{
    public partial class BookingForm : UserControl
    {
        private readonly MemberRepository memberRepo;
        private readonly MemberTypeRepository memberTypeRepo;
        private readonly SessionRepository sessionRepo;
        private readonly MemberGroupRepository memberGroupRepo;

        private BindingSource sessionBindingSource = new BindingSource();
        private BindingSource bookingBindingSource = new BindingSource();

        public BookingForm(
            MemberRepository memberRepository,
            MemberTypeRepository memberTypeRepository,
            SessionRepository sessionRepository,
            MemberGroupRepository memberGroupRepository
            )
        {
            this.memberRepo = memberRepository;
            this.memberTypeRepo = memberTypeRepository;
            this.sessionRepo = sessionRepository;
            this.memberGroupRepo = memberGroupRepository;

            InitializeComponent();

            if (memberSearch1 != null)
            {
                memberSearch1.Configure(memberRepo, memberTypeRepo, isReadOnly: true);
            }
        }
    }
}
