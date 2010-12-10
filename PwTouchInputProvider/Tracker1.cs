using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PwTouchInputProvider
{
    public class Tracker1 : TrackerBase
    {
        List<long> availableIds = new List<long>() { 0 };

        List<Blob> currentBlobs;

        public Tracker1()
        {
            currentBlobs = new List<Blob>();
        }

        public override List<Blob> ProcessBlobs(IEnumerable<Rectangle> newBlobs)
        {
            foreach (Blob blob in currentBlobs)
                blob.Active = false;

            bool blobTracked = false;
            foreach (Rectangle newBlob in newBlobs)
            {
                blobTracked = false;
                foreach (Blob prevBlob in currentBlobs)
                {
                    if (prevBlob.Rect.IntersectsWith(newBlob))
                    {
                        //We've found a previous blob that overlaps with a new one, so we consider them the same.
                        prevBlob.Rect = newBlob;
                        prevBlob.Active = true;
                        blobTracked = true;
                        break;
                    }
                }

                if (!blobTracked)
                {
                    if (availableIds.Count == 0)
                        availableIds.Add(currentBlobs.Count);

                    //We've found a new blob
                    Blob blob = new Blob()
                    {
                        Rect = newBlob,
                        Active = true,
                        Id = availableIds[0]
                    };
                    currentBlobs.Add(blob);

                    availableIds.RemoveAt(0);
                }
            }

            for (int i = 0; i < currentBlobs.Count; i++)
            {
                if (!currentBlobs[i].Active)
                {
                    availableIds.Add(currentBlobs[i].Id);
                    currentBlobs.RemoveAt(i);
                    
                    if (i > 0)
                        i--;
                }
            }

            return currentBlobs;
        }
    }
}
