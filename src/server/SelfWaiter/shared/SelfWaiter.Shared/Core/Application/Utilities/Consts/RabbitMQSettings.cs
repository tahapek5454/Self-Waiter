﻿namespace SelfWaiter.Shared.Core.Application.Utilities.Consts
{
    public static class RabbitMQSettings
    {
        #region DealerImageFile
        public const string StateMachine_DealerImageFileChangedQueue = "state_machine_dealer_image_file_changed_started_queue";
        public const string Dealer_DealerImageFileChangedQueue = "dealer_dealer_image_file_changed_queue";
        public const string Dealer_DealerImageFileChangedQueueError = "dealer_dealer_image_file_changed_queue_error";
        public const string File_DealerImageFileNotReceivedQueue = "file_dealer_image_file_not_received_queue";
        public const string File_DealerImageFileDeleteQueue = "file_dealer_image_file_delete_queue";
        #endregion
    }
}
