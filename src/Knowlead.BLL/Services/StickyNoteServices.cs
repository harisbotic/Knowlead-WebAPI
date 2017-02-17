using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DomainModel.LibraryModels;
using Knowlead.DTO.LibraryModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using static Knowlead.Common.Constants;

namespace Knowlead.Services
{
    public class StickyNoteServices : IStickyNoteServices
    {
        private readonly IStickyNoteRepository _stickyNoteRepository;

        public StickyNoteServices(IStickyNoteRepository stickyNoteRepository)
        {
            _stickyNoteRepository = stickyNoteRepository;
        }

        public async Task<StickyNote> Get(Guid applicationUserId, int stickyNoteId)
        {
            var stickyNote = await _stickyNoteRepository.Get(stickyNoteId);

            if(stickyNote == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(StickyNote));

            if(stickyNote.IsDeleted == true)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(StickyNote));

            //Check if is allowed to read
            if(!stickyNote.CreatedById.Equals(applicationUserId))
                throw new ErrorModelException(ErrorCodes.OwnershipError);

            return stickyNote;
        }

        public async Task<List<StickyNote>> GetAllFromUser(Guid applicationUserId)
        {
            var stickyNotes = await _stickyNoteRepository.GetAllWhere(stickyNote => stickyNote.CreatedById.Equals(applicationUserId));

            return stickyNotes;
        }

        public async Task<StickyNote> Create(Guid applicationUserId, StickyNoteModel stickyNoteModel)
        {
            var stickyNote = Mapper.Map<StickyNote>(stickyNoteModel);
            stickyNote.CreatedById = applicationUserId;
            
            _stickyNoteRepository.Add(stickyNote);

            await _stickyNoteRepository.Commit();

            return stickyNote;
        }

        public async Task<StickyNote> Patch(int stickyNoteId, JsonPatchDocument<StickyNoteModel> stickyNotePatch)
        {
            var stickyNote = await _stickyNoteRepository.Get(stickyNoteId);

            var stickyNoteModel = Mapper.Map<StickyNoteModel>(stickyNote);
            stickyNotePatch.ApplyTo(stickyNoteModel);

            stickyNote = Mapper.Map<StickyNoteModel, StickyNote>(stickyNoteModel, stickyNote);

            await _stickyNoteRepository.Commit();

            return stickyNote;
        }

        public async Task<Boolean> Delete(Guid applicationUserId, int stickyNoteId )
        {
            var stickyNote = await Get(applicationUserId, stickyNoteId);
            stickyNote.IsDeleted = true;
            
            _stickyNoteRepository.Update(stickyNote);
            await _stickyNoteRepository.Commit();
            return true;
        }
    }
}